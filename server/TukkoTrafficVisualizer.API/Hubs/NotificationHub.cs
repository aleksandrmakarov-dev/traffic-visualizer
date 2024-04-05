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
            _logger.LogInformation($"New client connected {Context.ConnectionId}");

            await Clients.Caller.SendAsync("ConnectionOpen", $"Connected {DateTime.UtcNow}");
        }
    }
}
