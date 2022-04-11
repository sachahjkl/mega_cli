using System.Globalization;
using MEGA_CLI.config;
using Microsoft.Extensions.Options;
using Cocona;

namespace MEGA_CLI.commands
{
    internal class CommandDemo : CoconaConsoleAppBase
    {
        private readonly IOptionsMonitor<Config> _monitor;
        private Config config => this._monitor.CurrentValue;

        public CommandDemo(IOptionsMonitor<Config> monitor)
        {
            _monitor = monitor;
        }

        [Command(Description = "Dire bonjour à quelqu'un.")]
        public void Hello([Argument(Description = "Personne à qui dire bonjour.")] String name = "Démo")
        {
            Console.WriteLine($"Hello {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name)} !");
        }

        [Command(Description = "Test du chargement de la config.")]
        public void TestConfig()
        {
            Hello(config.credentials.username);
        }

        [Command(Description = "Test du rechargement de la config à l'éxécution.")]
        public async Task TestConfigLoop()
        {
            try
            {
                while (!Context.CancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        this.TestConfig();
                    }
                    catch (Exception ex)
                    {
                        // error occured but continue to run(or terminate).
                        Console.WriteLine(ex.Message, "Found error");
                    }

                    // wait for next time
                    await Task.Delay(TimeSpan.FromSeconds(5), Context.CancellationToken);
                }
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                // you can write finally exception handling(without cancellation)
            }
        }
    }
}