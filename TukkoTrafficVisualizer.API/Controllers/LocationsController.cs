using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? query)
        {
            if(string.IsNullOrEmpty(query))
            {
                throw new BadRequestException("Search parameter 'query' is required");
            }

            IEnumerable<LocationResponse> locations = await _locationService.FindByQueryAsync(query);

            return Ok(locations);
        }
    }
}
