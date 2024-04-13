namespace TukkoTrafficVisualizer.Core.Options
{
    public class ApplicationOptions:IOptionsBase
    {
        public required string ClientBaseUrl { get; set; }
        public static string Name => "Application";
    }
}
