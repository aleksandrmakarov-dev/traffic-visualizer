namespace TukkoTrafficVisualizer.Core.Options
{
    public class GitlabOptions:IOptionsBase
    {
        public required string BaseUrl { get; set; }
        public required string AccessToken { get; set; }
        public required string ProjectId { get; set; }
        public static string Name => "Redis";
    }
}
