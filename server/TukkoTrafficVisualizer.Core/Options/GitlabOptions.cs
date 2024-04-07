namespace TukkoTrafficVisualizer.Core.Options
{
    public class GitlabOptions
    {
        public const string Name = "Gitlab";
        public required string BaseUrl { get; set; }
        public required string AccessToken { get; set; }
        public required string ProjectId { get; set; }
    }
}
