namespace TukkoTrafficVisualizer.API.Common
{
    public class Constants
    {
    }

    public enum WebSocketTopics
    {
        StationsUpdate,
        SensorsUpdate,
        RoadworksUpdate,
    }

    public enum SignalRMethods
    {
        ConnectionOpen,
        ConnectionClose,
        Message
    }
}
