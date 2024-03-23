using System.Net.WebSockets;
using TukkoTrafficVisualizer.Infrastructure.Models;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IWebSocketManagerService
    {
        Task SendAsync(WebSocketMessage message);
        Task SendAsync(string key, WebSocketMessage message);
        string AddSocket(WebSocket socket);
        Task RemoveSocketAsync(string key);
    }
}
