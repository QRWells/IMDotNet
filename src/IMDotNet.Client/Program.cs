using IMDotNet.Client;
using IMDotNet.Client.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = loggerFactory.CreateLogger<Program>();
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var settings = config.GetRequiredSection("Settings").Get<Settings>();

// TODO: Add command args parse
var address = settings.Address;
var port = settings.Port;

logger.LogInformation("TCP server address: {}", address);
logger.LogInformation("TCP server port: {}", port);

var client = new Client(address, port);

logger.LogInformation("Client connecting...");
client.ConnectAsync();
logger.LogInformation("Done!");

logger.LogInformation("Press Enter to stop the client or '!' to reconnect the client...");

while (true)
{
    var line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
        break;

    if (line == "!")
    {
        logger.LogInformation("Client disconnecting...");
        client.DisconnectAsync();
        logger.LogInformation("Done!");
        continue;
    }

    client.SendAsync(line);
}

logger.LogInformation("Client disconnecting...");
client.DisconnectAndStop();
logger.LogInformation("Done!");