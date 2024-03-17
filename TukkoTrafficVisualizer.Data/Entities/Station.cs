using Redis.OM.Modeling;

namespace TukkoTrafficVisualizer.Data.Entities
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

    [Document(Prefixes = [nameof(Station)], StorageType = StorageType.Json)]
    public class Station:Entity
    {
        [Indexed(Sortable = true)]
        public int TmsNumber { get; set; }
        public DateTime DataUpdatedTime { get; set; }
        public string Name { get; set; }
        public Names Names { get; set; }
        public Coordinates Coordinates { get; set; }
        public int RoadNumber { get; set; }
        public int RoadSection { get; set; }
        public string Carriageway { get; set; }
        public string Side { get; set; }
        public string Municipality { get; set; }
        public int MunicipalityCode { get; set; }
        public string Province { get; set; }
        public int ProvinceCode { get; set; }
        public string Direction1Municipality { get; set; }
        public int Direction1MunicipalityCode { get; set; }
        public string Direction2Municipality { get; set; }
        public int Direction2MunicipalityCode { get; set; }
        public double? FreeFlowSpeed1 { get; set; }
        public double? FreeFlowSpeed2 { get; set; }
    }
}
