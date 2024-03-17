using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Services;

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
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Sensor> sensors = await _sensorService.GetAsync();

            return Ok(sensors);
        }
    }
}
