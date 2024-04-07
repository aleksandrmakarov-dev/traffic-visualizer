using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Requests
{
    public class AddFavoriteStationRequest
    {
        [Required]
        public required string stationId { get; set; }
    }
}
