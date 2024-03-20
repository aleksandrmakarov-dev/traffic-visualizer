namespace TukkoTrafficVisualizer.Infrastructure.Models.Responses
{
    public class EmailVerificationResponse
    {
        public string Id { get; set; }
        public required string Email { get; set; }
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiresAt { get; set; }
    }
}
