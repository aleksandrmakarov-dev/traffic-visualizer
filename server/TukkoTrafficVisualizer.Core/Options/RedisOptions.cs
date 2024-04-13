namespace TukkoTrafficVisualizer.Core.Options
{
    public class RedisOptions
    {
        public const string Name = "Redis";
        public required string Development { get; set; }
        public required string Production { get; set; }
        public int Port { get; set; }
        public required string Password { get; set; }
    }
}
