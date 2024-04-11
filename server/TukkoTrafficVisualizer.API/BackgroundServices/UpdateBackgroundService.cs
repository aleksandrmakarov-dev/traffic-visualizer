using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using TukkoTrafficVisualizer.API.Common;
using TukkoTrafficVisualizer.API.Hubs;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class UpdateBackgroundService:BackgroundService
    {
        private readonly int _shortPeriodMinutes = 2;
        private readonly int _longPeriodMinutes = 10;
        private int _minutesLeft;
        private bool _saveHistoryTime;

        private readonly ILogger<UpdateBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public UpdateBackgroundService(ILogger<UpdateBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMinutes(_shortPeriodMinutes));
            do
            {
                try
                {
                    _saveHistoryTime = _minutesLeft <= 0;

                    Stopwatch sw = Stopwatch.StartNew();

                    _logger.LogInformation($"{DateTime.UtcNow}: UpdateBackgroundService is working!");

                    await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();

                    await UpdateRoadworksAsync(scope, sw);
                    await UpdateSensorsAsync(scope, sw);
                    await UpdateStationsAsync(scope, sw);

                    if (_saveHistoryTime)
                    {
                        _minutesLeft = _longPeriodMinutes - _shortPeriodMinutes;
                    }
                    else
                    {
                        _minutesLeft -= _shortPeriodMinutes;
                    }

                    sw.Stop();

                    IHubContext<NotificationHub> hubContext =
                        scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();

                    await hubContext.Clients.All.SendAsync(SignalRMethods.Message.ToString(), DateTime.UtcNow,CancellationToken.None);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to execute PeriodicHostedService with exception message: {ex.Message}.");
                }

            } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
        }

        private async Task UpdateRoadworksAsync(AsyncServiceScope scope, Stopwatch sw)
        {
            sw.Restart();

            IRoadworkCacheService roadworkService = scope.ServiceProvider.GetRequiredService<IRoadworkCacheService>();
            IRoadworkHttpService roadworkHttpService = scope.ServiceProvider.GetRequiredService<IRoadworkHttpService>();
            scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();
            
            RoadworkContract roadworkContract = await roadworkHttpService.FetchAsync();

            await roadworkService.SaveRoadworksAsync(roadworkContract);

            _logger.LogInformation($"Roadworks have been updated. It took {sw.ElapsedMilliseconds} ms");
        }

        private async Task UpdateSensorsAsync(AsyncServiceScope scope, Stopwatch sw)
        {
            sw.Restart();

            ISensorCacheService sensorCacheService = scope.ServiceProvider.GetRequiredService<ISensorCacheService>();
            ISensorHttpService sensorHttpService = scope.ServiceProvider.GetRequiredService<ISensorHttpService>();
            ISensorService sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
            scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();

            SensorContract sensorContract = await sensorHttpService.FetchAsync();

            await sensorCacheService.SaveSensorsAsync(sensorContract);

            _logger.LogInformation($"Sensors have been updated. It took {sw.ElapsedMilliseconds} ms");

            if (_saveHistoryTime)
            {
                sw.Restart();

                await sensorService.SaveAsync(sensorContract);

                _logger.LogInformation($"Sensors have been stored to database. It took {sw.ElapsedMilliseconds} ms");
            }

        }

        private async Task UpdateStationsAsync(AsyncServiceScope scope, Stopwatch sw)
        {
            sw.Restart();

            IStationCacheService stationCacheService = scope.ServiceProvider.GetRequiredService<IStationCacheService>();
            IStationHttpService stationHttpService = scope.ServiceProvider.GetRequiredService<IStationHttpService>();
            IStationService stationService = scope.ServiceProvider.GetRequiredService<IStationService>();

            StationContract stationContract = await stationHttpService.FetchAsync();
            
            await stationCacheService.SaveStationsAsync(stationContract);

            _logger.LogInformation($"Stations have been updated. It took {sw.ElapsedMilliseconds} ms");

            if (_saveHistoryTime)
            {
                sw.Restart();
                await stationService.SaveAsync(stationContract);

                _logger.LogInformation($"Stations have been stored to database. It took {sw.ElapsedMilliseconds} ms");
            }
        }
    }
}
