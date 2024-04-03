using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorCacheService _sensorService;

        public SensorsController(ISensorCacheService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string[]? ids = null, [FromQuery] string? stationId =  null)
        {
            IEnumerable<Cache.Entities.Sensor> sensors = await _sensorService.GetAsync(ids,stationId);

            return Ok(sensors);
        }
    }
}
