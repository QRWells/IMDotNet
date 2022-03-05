using IMDotNet.ASPNETServer;
using IMDotNet.ASPNETServer.Handlers;
using IMDotNet.ASPNETServer.Services;
using IMDotNet.Shared.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebHost.CreateDefaultBuilder(args);

builder.ConfigureKestrel(
    options =>
    {
        options.ListenLocalhost(8007, listenOptions =>
        {
            listenOptions.UseConnectionLogging();
            listenOptions.UseConnectionHandler<TcpHandler>();
        });
    });

builder.ConfigureLogging((context, loggingBuilder) =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
});

builder.ConfigureServices(collection =>
{
    collection.TryAddSingleton<IMessageParser, MessageParser>();
    collection.TryAddSingleton<IAuthentication<User>, Authenticator>();
});

builder.UseStartup(typeof(StartUp));

var app = builder.Build();

app.Run();