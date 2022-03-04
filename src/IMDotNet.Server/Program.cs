using System.Net;
using IMDotNet.Server;
using IMDotNet.Server.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = loggerFactory.CreateLogger<Program>();
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var settings = config.GetRequiredSection("Settings").Get<Settings>();

logger.LogInformation("Current directory {}", Directory.GetCurrentDirectory());

var port = settings.Port;

logger.LogInformation("TCP server port: {}", port);

var server = new Server(IPAddress.Any, port);

logger.LogInformation("Server starting...");

server.Start();

logger.LogInformation("Done!");
logger.LogInformation("Press Enter to stop the server or '!' to restart the server...");

// Execute commands from server
while (true)
{
    var line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
        break;

    if (line == "!")
    {
        Console.Write("Server restarting...");
        server.Restart();
        logger.LogInformation("Done!");
        continue;
    }

    // Multicast admin message to all sessions
    line = "(admin) " + line;
    server.Multicast(line);
}

logger.LogInformation("Server stopping...");
server.Stop();
logger.LogInformation("Done!");