using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class WebSocketManagerService : IWebSocketManagerService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ConcurrentDictionary<string, WebSocket> _sockets;

        public WebSocketManagerService()
        {
            _sockets = new ConcurrentDictionary<string, WebSocket>();

            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task SendAsync(WebSocketMessage message)
        {
            string json = JsonSerializer.Serialize(message);

            foreach (WebSocket ws in _sockets.Values)
            {
                await SendMessageAsync(ws, message);
            }
        }

        public async Task SendAsync(string key, WebSocketMessage message)
        {
            WebSocket? ws = GetByKey(key);

            if (ws == null) return;

            await SendMessageAsync(ws, message);
        }

        public string AddSocket(WebSocket socket)
        {
            string key = CreateConnectionId();

            _sockets.TryAdd(key, socket);

            return key;
        }

        public async Task RemoveSocketAsync(string key)
        {
            _sockets.TryRemove(key, out WebSocket? ws);

            if (ws == null) return;

            if (ws.State != WebSocketState.Open) return;

            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing web socket on server",
                CancellationToken.None);
        }

        public WebSocket? GetByKey(string key)
        {
            return _sockets.FirstOrDefault(s => s.Key == key).Value;
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }

        private async Task SendMessageAsync(WebSocket ws, WebSocketMessage message)
        {
            if (ws.State == WebSocketState.Closed)
            {
                return;
            }

            string serializedMessage = JsonSerializer.Serialize(message, _jsonSerializerOptions);
            byte[] encodedMessage = Encoding.UTF8.GetBytes(serializedMessage);

            await ws.SendAsync(
                new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
                );
        }
    }
}
