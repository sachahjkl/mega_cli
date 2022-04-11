using Cocona;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MEGA_CLI.config;

namespace MEGA_CLI.commands
{
    internal class Ls
    {
        private readonly IOptionsMonitor<Config> _monitor;
        private Config config => this._monitor.CurrentValue;
        private readonly ILogger<Ls> logger;

        public Ls(IOptionsMonitor<Config> monitor, ILogger<Ls> logger)
        {
            _monitor = monitor;
            this.logger = logger;
        }

        [Command(Description = "Liste les fichiers/dossiers depuis un chemin donné.")]
        public void ls(string path = "./")
        {
            var fullPath = Path.GetDirectoryName(Path.GetFullPath(path));
            logger.LogInformation("Contenu du répertoire {0}", fullPath);
            Console.WriteLine($"Répertoire : {fullPath}");

            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                foreach (var dir in dirs)
                {
                    Console.WriteLine(Format($"{dir}{Path.DirectorySeparatorChar}"));
                }

                foreach (var file in files)
                {
                    Console.WriteLine(Format(file));
                }
            }
            else
            {
                Console.WriteLine(Format(Path.GetFileName(path)));
                return;

            }

        }

        private string Format(string path)
        {

            return $"- {path}";

        }
    }
}