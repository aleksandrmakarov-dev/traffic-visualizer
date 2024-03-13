using StackExchange.Redis;
using TukkoTrafficVisualizer.Data.Redis.Entities;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class IndexCreationService:BackgroundService
    {
        private readonly ILogger<IndexCreationService> _logger;

        public IndexCreationService(ILogger<IndexCreationService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Roadwork index created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
