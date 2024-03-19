using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

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
            [FromQuery] DateTime startTimeOnAfter = default, 
            [FromQuery] DateTime startTimeOnBefore = default, 
            [FromQuery] int primaryPointRoadNumber = 8102, 
            [FromQuery] int primaryPointRoadSection = 1, 
            [FromQuery] int secondaryPointRoadNumber = 8102, 
            [FromQuery] int secondaryPointRoadSection = 5, 
            [FromQuery] string severity = "HIGH"
            )
        {
            IEnumerable<Roadwork> roadworkList = await _roadworkService.GetAsync(
                primaryPointRoadNumber,
                primaryPointRoadSection,
                secondaryPointRoadNumber,
                secondaryPointRoadSection,
                severity
                );

            return Ok(roadworkList);
        }
    }
}
