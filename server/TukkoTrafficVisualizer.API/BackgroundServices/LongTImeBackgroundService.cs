using System.Diagnostics;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class LongTimeBackgroundService:BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);
        private readonly ILogger<LongTimeBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public LongTimeBackgroundService(ILogger<LongTimeBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            do
            {
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    _logger.LogInformation("LongTimeBackgroundService is working!");

                    await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();

                    await UpdateStationsAsync(scope, sw);

                    sw.Stop();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to execute PeriodicHostedService with exception message: {ex.Message}.");
                }

            } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
        }

        private async Task UpdateStationsAsync(AsyncServiceScope scope, Stopwatch sw)
        {
            sw.Restart();

            IStationService stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
            IStationHttpService stationHttpService = scope.ServiceProvider.GetRequiredService<IStationHttpService>();

            StationContract stationContract = await stationHttpService.FetchAsync();

            await stationService.SaveStationsAsync(stationContract);

            _logger.LogInformation($"Stations have been updated. It took {sw.ElapsedMilliseconds} ms");

        }
    }
}
