using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationCacheService _stationService;

        public StationsController(IStationCacheService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Cache.Entities.Station> foundStations = await _stationService.GetCacheAllAsync();

            return Ok(foundStations);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            Cache.Entities.Station? foundStation = await _stationService.GetCacheByIdAsync(id);

            if (foundStation == null)
            {
                throw new NotFoundException($"Station {id} not found");
            }

            return Ok(foundStation);
        }
    }
}
