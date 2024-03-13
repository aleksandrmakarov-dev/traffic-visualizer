using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoadworksController : ControllerBase
    {
        private readonly IRoadworkService _roadworkService;

        public RoadworksController(IRoadworkService roadworkService)
        {
            _roadworkService = roadworkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] DateTime startTimeOnAfter, 
            [FromQuery] DateTime startTimeOnBefore, 
            [FromQuery] int primaryPointRoadNumber = 8102, 
            [FromQuery] int primaryPointRoadSection = 1, 
            [FromQuery] int secondaryPointRoadNumber = 8102, 
            [FromQuery] int secondaryPointRoadSection = 5, 
            [FromQuery] string severity = "HIGH"
            )
        {
            IEnumerable<Data.Redis.Entities.Roadwork> roadworkList = await _roadworkService.FilterAsync(
                primaryPointRoadNumber, primaryPointRoadSection, secondaryPointRoadNumber, secondaryPointRoadSection,
                startTimeOnAfter, startTimeOnBefore, severity);

            return Ok(roadworkList);
        }
    }
}
