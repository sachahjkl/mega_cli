namespace MEGA_CLI.config
{
    internal class Config
    {
        public Credentials credentials { get; set; } = new Credentials();
    }

    internal class Credentials
    {
        public String username { get; set; } = String.Empty;
        public String password { get; set; } = String.Empty;
        public String megaField { get; set; } = String.Empty;
    }
}