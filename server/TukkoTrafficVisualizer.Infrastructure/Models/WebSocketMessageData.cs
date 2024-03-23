using System.Text.Json.Serialization;

namespace TukkoTrafficVisualizer.Infrastructure.Models;

public class WebSocketMessageData
{
    [JsonPropertyName("topic")]
    public required string Topic { get; set; }
    [JsonPropertyName("payload")]
    public string Payload { get; set; }
}