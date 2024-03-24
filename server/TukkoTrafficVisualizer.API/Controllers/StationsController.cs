using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Station?> foundStations = await _stationService.GetAllAsync();

            return Ok(foundStations);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            Data.Entities.Station? foundStation = await _stationService.GetByIdAsync(id);

            if (foundStation == null)
            {
                throw new NotFoundException($"Station {id} not found");
            }

            return Ok(foundStation);
        }
    }
}
