
using TukkoTrafficVisualizer.Data.Repositories;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class IndexCreationService:BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<IndexCreationService> _logger;

        public IndexCreationService(ILogger<IndexCreationService> logger, IServiceScopeFactory scopeFactory)
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
