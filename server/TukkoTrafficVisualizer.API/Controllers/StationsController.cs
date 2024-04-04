using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationCacheService _stationCacheService;
        private readonly IStationService _stationService;

        public StationsController(IStationCacheService stationService, IStationService stationService1)
        {
            _stationCacheService = stationService;
            _stationService = stationService1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Cache.Entities.Station> foundStations = await _stationCacheService.GetCacheAllAsync();

            return Ok(foundStations);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            Cache.Entities.Station? foundStation = await _stationCacheService.GetCacheByIdAsync(id);

            if (foundStation == null)
            {
                throw new NotFoundException($"Station {id} not found");
            }

            return Ok(foundStation);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistoryById([FromRoute] string id)
        {
            Database.Entities.Station? foundStation = await _stationService.GetByIdAsync(id);

            if (foundStation == null)
            {
                throw new NotFoundException($"Station {id} not found");
            }

            return Ok(foundStation);
        }
    }
}
