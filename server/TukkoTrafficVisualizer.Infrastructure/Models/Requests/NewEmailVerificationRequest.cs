using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Requests 
{
    public class NewEmailVerificationRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
