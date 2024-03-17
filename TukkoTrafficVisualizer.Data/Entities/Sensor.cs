using Redis.OM.Modeling;

namespace TukkoTrafficVisualizer.Data.Entities
{
    [Document(Prefixes = [nameof(Sensor),], StorageType = StorageType.Json)]
    public class Sensor:Entity
    {
        [Indexed(Sortable = true)]
        public int SensorId { get; set; }
        [Indexed(Sortable = true)]
        public int StationId { get; set; }
        public string Name { get; set; }
        [Indexed(Sortable = true)]
        public DateTime? TimeWindowStart { get; set; }
        [Indexed(Sortable = true)]
        public DateTime? TimeWindowEnd { get; set; }
        public DateTime? MeasuredTime { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }
    }
}
