using Microsoft.AspNetCore.SignalR;

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

            await Clients.Caller.SendAsync("ConnectionOpen", $"Connected {Context.ConnectionId} {DateTime.UtcNow}");

            await Clients.Others.SendAsync("ClientJoined",
                $"New client joined {Context.ConnectionId}, {DateTime.UtcNow}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);

            await Clients.Others.SendAsync("ClientDisconnected",
                $"Disconnected {Context.ConnectionId} {DateTime.UtcNow}");
        }
    }
}
