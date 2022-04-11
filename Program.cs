using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cocona;
using ZLogger;
using Cysharp.Text;
using MEGA_CLI.commands;
using MEGA_CLI.config;

var builder = CoconaApp.CreateBuilder();

var prefixFormat = ZString.PrepareUtf8<LogLevel, DateTime>("[{0}][{1}] ");
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddConsole();
builder.Logging.AddZLoggerFile("./log/dev.log", (options) =>
{
    options.PrefixFormatter = (writer, info) =>
        prefixFormat.FormatTo(ref writer, info.LogLevel, info.Timestamp.DateTime.ToLocalTime());
}); // add ZLogger file output
builder.Logging.AddDebug();

var configFileLocation = Environment.GetEnvironmentVariable("CONFIG_FILE_LOCATION") ?? "config.yml";
builder.Configuration.AddYamlFile(path: configFileLocation, optional: false, reloadOnChange: true);
builder.Services.Configure<Config>(builder.Configuration);
// builder.Services.AddTransient<Ls>();




var app = builder.Build();
app.AddCommands<Ls>();
app.AddCommands<CommandDemo>();

// exemple sous commande :
app.AddSubCommand("demo", (cmd) =>
{
    cmd.AddCommands<CommandDemo>();
}).WithDescription("Commande avec sous commandes");
app.Run();