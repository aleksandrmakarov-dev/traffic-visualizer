using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TukkoTrafficVisualizer.API.Attributes;
using TukkoTrafficVisualizer.Core.Constants;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly ILogger<StationsController> _logger;
        private readonly IStationCacheService _stationCacheService;
        private readonly IUsersService _usersService;
        private readonly IStationService _stationService;

        public StationsController(IStationCacheService stationService, IStationService stationService1, IUsersService usersService, ILogger<StationsController> logger)
        {
            _stationCacheService = stationService;
            _stationService = stationService1;
            _usersService = usersService;
            _logger = logger;
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
        public async Task<IActionResult> GetHistoryById([FromRoute] string id, [FromQuery] TimeRange? timeRange)
        {
            if (timeRange == null)
            {
                throw new BadRequestException("TimeRange is not specified");
            }

            _logger.LogInformation(timeRange.ToString());

            Database.Entities.Station? foundStation = await _stationService.GetHistoryByIdAsync(id,timeRange.Value);

            if (foundStation == null)
            {
                throw new NotFoundException($"Station {id} not found");
            }

            return Ok(foundStation);
        }

        [Authorize]
        [HttpGet("favorite")]
        public async Task<IActionResult> GetFavoriteStations()
        {
            JwtPayload user = (JwtPayload)HttpContext.Items[Constants.UserContextName]!;

            IEnumerable<string> foundFavoriteStations = await _usersService.GetFavoriteStationsAsync(user.Id);

            return Ok(foundFavoriteStations);
        }

        [Authorize]
        [HttpPost("favorite")]
        public async Task<IActionResult> AddFavoriteStation([FromBody] AddFavoriteStationRequest request)
        {
            JwtPayload user = (JwtPayload)HttpContext.Items[Constants.UserContextName]!;

            await _usersService.AddFavoriteStationsAsync(user.Id, request.stationId);

            return Ok();
        }

        [Authorize]
        [HttpDelete("favorite/{stationId}")]
        public async Task<IActionResult> RemoveFavoriteStationAsync([FromRoute] string stationId)
        {
            JwtPayload user = (JwtPayload)HttpContext.Items[Constants.UserContextName]!;

            await _usersService.RemoveFavoriteStationAsync(user.Id, stationId);

            return NoContent();
        }
    }
}
