namespace Shared.Extensions.Config
{
    public class RedisSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public int ExpiresIn { get; set; }
    }
}
