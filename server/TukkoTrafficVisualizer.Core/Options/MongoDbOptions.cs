namespace TukkoTrafficVisualizer.Core.Options;

public class MongoDbOptions
{
    public const string Name = "MongoDb";
    public required string Development { get; set; }
    public required string Production { get; set;}
    public required string Username { get; set; }
    public required string Password { get; set; }

}