
using System.Text.Json.Serialization;

namespace TukkoTrafficVisualizer.Infrastructure.Models
{
    public enum MessageType
    {
        Text,
        ConnectionEvent,
    }

    public class WebSocketMessage
    {
        [JsonPropertyName("message_type")]
        public MessageType MessageType { get; set; }
        [JsonPropertyName("data")]
        public required WebSocketMessageData Data { get; set; }
    }
}
