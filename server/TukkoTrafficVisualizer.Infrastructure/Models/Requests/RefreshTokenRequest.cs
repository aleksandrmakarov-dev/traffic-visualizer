using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Requests;

public class RefreshTokenRequest
{
    [Required]
    public required string Token { get; set; }
}