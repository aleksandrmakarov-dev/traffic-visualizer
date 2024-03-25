using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Requests
{
    public class CreateFeedbackRequest
    {
        [Required]
        [MinLength(5)]
        public required string Title { get; set; }
        [Required]
        [MinLength(20)]
        public required string Description { get; set; }
    }
}
