using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Requests
{
    public class SignOutRequest
    {
        [Required]
        public required string Token { get; set; }
    }
}
