namespace TukkoTrafficVisualizer.Database.Entities
{
    public class Coordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class Names
    {
        public string Fi { get; set; }
        public string Sv { get; set; }
        public string En { get; set; }
    }

    public class Station:Entity
    {
        public required string StationId { get; set; }
        public int TmsNumber { get; set; }
        public DateTime DataUpdatedTime { get; set; }
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }
        public IEnumerable<Sensor> Sensors { get; set; }
    }
}
