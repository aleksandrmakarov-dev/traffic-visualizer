namespace TukkoTrafficVisualizer.Core.Options;

public class JsonWebTokenOptions:IOptionsBase
{
    public required string SecretKey { get; set; }
    public static string Name => "JsonWebToken";
}