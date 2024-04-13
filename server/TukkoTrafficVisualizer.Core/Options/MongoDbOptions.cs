namespace TukkoTrafficVisualizer.Core.Options;

public class MongoDbOptions:IOptionsBase
{
    public required string Development { get; set; }
    public required string Production { get; set;}
    public required string Username { get; set; }
    public required string Password { get; set; }
    public static string Name => "MongoDb";
}