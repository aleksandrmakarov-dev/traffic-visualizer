using Microsoft.AspNetCore.SignalR;
using TukkoTrafficVisualizer.API.Common;

namespace TukkoTrafficVisualizer.API.Hubs
{
    public class NotificationHub:Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            _logger.LogInformation($"New client connected {Context.ConnectionId}");

            await Clients.Caller.SendAsync(SignalRMethods.ConnectionOpen.ToString(), $"Connected {Context.ConnectionId} {DateTime.UtcNow}");

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);

            await Clients.Caller.SendAsync(SignalRMethods.ConnectionClose.ToString(),
                $"Disconnected {Context.ConnectionId} {DateTime.UtcNow}");
        }
    }
}
