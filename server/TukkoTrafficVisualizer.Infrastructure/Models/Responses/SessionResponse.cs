namespace TukkoTrafficVisualizer.Infrastructure.Models.Responses
{
    public class SessionResponse
    {
        public required string AccessToken { get; set; }
        public string UserId { get; set; }
        public required string Email { get; set; }
        public Role Role { get; set; }
    }
}
