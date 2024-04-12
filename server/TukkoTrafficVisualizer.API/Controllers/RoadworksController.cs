using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoadworksController : ControllerBase
    {
        private readonly IRoadworkCacheService _roadworkService;

        public RoadworksController(IRoadworkCacheService roadworkService)
        {
            _roadworkService = roadworkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Cache.Entities.Roadwork> roadworkList = await _roadworkService.GetAsync();

            return Ok(roadworkList);
        }
    }
}
