namespace TukkoTrafficVisualizer.Infrastructure.Models.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
