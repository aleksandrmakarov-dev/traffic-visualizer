
using Redis.OM.Contracts;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class UpdateCacheBackgroundService:BackgroundService
    {
        // update delay
        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);
        private readonly ILogger<UpdateCacheBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public UpdateCacheBackgroundService(ILogger<UpdateCacheBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                DateTime start = DateTime.UtcNow;

                _logger.LogInformation("UpdateCacheBackgroundService is working!");
                await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();

                DateTime before = DateTime.UtcNow;
                await UpdateRoadworksAsync(scope);
                DateTime after = DateTime.Now;

                _logger.LogInformation($"Roadwork data has been updated. It took {(after-before).Seconds}s");

                before = DateTime.UtcNow;
                await UpdateSensorsAsync(scope);
                after = DateTime.Now;

                _logger.LogInformation($"Sensor data has been updated. It took {(after-before).Seconds}s");

                before = DateTime.UtcNow;
                await UpdateStationsAsync(scope);
                after = DateTime.Now;

                _logger.LogInformation($"Station data has been updated. It took {(after-before).Seconds}s");
                _logger.LogInformation($"In total it took {(after-start).Seconds}s");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to execute PeriodicHostedService with exception message: {ex.Message}.");
            }

            //using PeriodicTimer timer = new PeriodicTimer(_period);
            //while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            //{
            //    
            //}
        }

        private async Task UpdateRoadworksAsync(AsyncServiceScope scope)
        {
            IRoadworkService roadworkService = scope.ServiceProvider.GetRequiredService<IRoadworkService>();
            RoadworkContract roadworkContract = await roadworkService.FetchRoadworkAsync();
            await roadworkService.SaveRoadworksAsync(roadworkContract);
        }

        private async Task UpdateSensorsAsync(AsyncServiceScope scope)
        {
            ISensorService sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
            SensorContract sensorContract = await sensorService.FetchSensorsAsync();
            await sensorService.SaveSensorsAsync(sensorContract);
        }

        private async Task UpdateStationsAsync(AsyncServiceScope scope)
        {
            IStationService stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
            StationContract stationContract = await stationService.FetchStationsAsync();
            await stationService.SaveStationsAsync(stationContract);
        }
    }
}
