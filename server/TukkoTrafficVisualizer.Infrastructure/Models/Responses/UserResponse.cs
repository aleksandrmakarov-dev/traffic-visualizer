namespace TukkoTrafficVisualizer.Infrastructure.Models.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
