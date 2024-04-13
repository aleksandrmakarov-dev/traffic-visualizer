namespace TukkoTrafficVisualizer.Core.Options
{
    public class RedisOptions:IOptionsBase
    {
        public required string Development { get; set; }
        public required string Production { get; set; }
        public int Port { get; set; }
        public required string Password { get; set; }
        public static string Name => "Redis";
    }
}
