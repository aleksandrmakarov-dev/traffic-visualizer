using System.Diagnostics;
using System.Globalization;
using TukkoTrafficVisualizer.API.Common;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.API.BackgroundServices
{
    public class UpdateCacheBackgroundService:BackgroundService
    {
        // update delay
        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);
        private readonly ILogger<UpdateCacheBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebSocketManagerService _websocketManagerService;

        public UpdateCacheBackgroundService(ILogger<UpdateCacheBackgroundService> logger, IServiceScopeFactory scopeFactory, IWebSocketManagerService websocketManagerService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _websocketManagerService = websocketManagerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            do
            {
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    _logger.LogInformation("UpdateCacheBackgroundService is working!");

                    await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();

                    await UpdateRoadworksAsync(scope, sw);
                    await UpdateSensorsAsync(scope, sw);
                    await UpdateStationsAsync(scope, sw);

                    sw.Stop();
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

            IRoadworkService roadworkService = scope.ServiceProvider.GetRequiredService<IRoadworkService>();
            RoadworkContract roadworkContract = await roadworkService.FetchRoadworkAsync();
            await roadworkService.SaveRoadworksAsync(roadworkContract);

            _logger.LogInformation($"Roadworks have been updated. It took {sw.ElapsedMilliseconds} ms");

            await SendUpdateMessageAsync(WebSocketTopics.RoadworksUpdate, DateTime.UtcNow);
        }

        private async Task UpdateSensorsAsync(AsyncServiceScope scope, Stopwatch sw)
        {
            sw.Restart();

            ISensorService sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
            SensorContract sensorContract = await sensorService.FetchSensorsAsync();
            await sensorService.SaveSensorsAsync(sensorContract);

            _logger.LogInformation($"Sensors have been updated. It took {sw.ElapsedMilliseconds} ms");

            await SendUpdateMessageAsync(WebSocketTopics.SensorsUpdate, DateTime.UtcNow);
        }

        private async Task UpdateStationsAsync(AsyncServiceScope scope, Stopwatch sw)
        {
            sw.Restart();

            IStationService stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
            StationContract stationContract = await stationService.FetchStationsAsync();
            await stationService.SaveStationsAsync(stationContract);

            _logger.LogInformation($"Stations have been updated. It took {sw.ElapsedMilliseconds} ms");

            await SendUpdateMessageAsync(WebSocketTopics.StationsUpdate, DateTime.UtcNow);
        }

        private async Task SendUpdateMessageAsync(WebSocketTopics topic, DateTime date)
        {
            await _websocketManagerService.SendAsync(new WebSocketMessage
            {
                MessageType = MessageType.Text,
                Data = new WebSocketMessageData
                {
                    Topic = topic.ToString(),
                    Payload = date.ToString(CultureInfo.InvariantCulture)
                }
            });
        }
    }
}
