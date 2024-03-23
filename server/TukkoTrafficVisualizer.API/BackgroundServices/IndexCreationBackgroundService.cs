using TukkoTrafficVisualizer.Data.Interfaces;
using TukkoTrafficVisualizer.Data.Repositories;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class IndexCreationBackgroundService:BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<IndexCreationBackgroundService> _logger;

        public IndexCreationBackgroundService(ILogger<IndexCreationBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();

                IRoadworkCacheRepository roadworkRepository = scope.ServiceProvider.GetRequiredService<IRoadworkCacheRepository>();

                await roadworkRepository.CreateIndexAsync();

                _logger.LogInformation("Roadwork index created");

                ISensorCacheRepository sensorRepository = scope.ServiceProvider.GetRequiredService<ISensorCacheRepository>();

                await sensorRepository.CreateIndexAsync();

                _logger.LogInformation("Sensor index created");

                IStationCacheRepository stationRepository = scope.ServiceProvider.GetRequiredService<IStationCacheRepository>();

                await stationRepository.CreateIndexAsync();

                _logger.LogInformation("Station index created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
