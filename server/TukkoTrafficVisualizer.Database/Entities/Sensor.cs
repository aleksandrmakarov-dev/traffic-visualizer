namespace TukkoTrafficVisualizer.Database.Entities
{
    public class Sensor:Entity
    {
        public required string SensorId { get; set; }
        public required string StationId { get; set; }
        public required string Name { get; set; }
        public DateTime? TimeWindowStart { get; set; }
        public DateTime? TimeWindowEnd { get; set; }
        public DateTime? MeasuredTime { get; set; }
        public string? Unit { get; set; }
        public double Value { get; set; }
    }
}
