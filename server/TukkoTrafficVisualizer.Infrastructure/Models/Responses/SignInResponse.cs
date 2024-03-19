namespace TukkoTrafficVisualizer.Infrastructure.Models.Responses
{
    public class SignInResponse
    {
        public required string RefreshToken { get; set; }
        public required SessionResponse Session { get; set; }
    }
}
