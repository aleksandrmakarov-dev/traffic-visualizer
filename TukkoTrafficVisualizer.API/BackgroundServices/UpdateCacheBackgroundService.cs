
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
                _logger.LogInformation("UpdateCacheBackgroundService is working!");

                await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();

                IRoadworkService roadworkService = scope.ServiceProvider.GetRequiredService<IRoadworkService>();

                RoadworkContract? roadworkContract = await roadworkService.FetchLatestRoadworkAsync();

                if (roadworkContract == null)
                {
                    _logger.LogInformation("Roadwork data is empty");
                    return;
                }

                await roadworkService.SaveRoadworkAsync(roadworkContract);

                _logger.LogInformation("Roadwork data has been updated");

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
    }
}
