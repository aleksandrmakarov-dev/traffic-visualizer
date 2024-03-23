using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string[]? ids = null)
        {
            IEnumerable<Sensor> sensors = await _sensorService.GetAsync(ids);

            return Ok(sensors);
        }
    }
}
