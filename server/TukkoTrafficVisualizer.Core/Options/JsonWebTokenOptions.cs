namespace TukkoTrafficVisualizer.Core.Options;

public class JsonWebTokenOptions
{
    public const string Name = "JsonWebToken";
    public required string SecretKey { get; set; }
}