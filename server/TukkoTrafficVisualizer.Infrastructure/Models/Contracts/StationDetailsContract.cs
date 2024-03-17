using System.Text.Json.Serialization;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Contracts
{
    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class Names
    {
        [JsonPropertyName("fi")]
        public string Fi { get; set; }

        [JsonPropertyName("sv")]
        public string? Sv { get; set; }

        [JsonPropertyName("en")]
        public string? En { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tmsNumber")]
        public int TmsNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("collectionStatus")]
        public string CollectionStatus { get; set; }

        [JsonPropertyName("state")]
        public object State { get; set; }

        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }

        [JsonPropertyName("collectionInterval")]
        public int CollectionInterval { get; set; }

        [JsonPropertyName("names")]
        public Names Names { get; set; }

        [JsonPropertyName("roadAddress")]
        public StationRoadAddress RoadAddress { get; set; }

        [JsonPropertyName("liviId")]
        public string LiviId { get; set; }

        [JsonPropertyName("country")]
        public object Country { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("repairMaintenanceTime")]
        public object RepairMaintenanceTime { get; set; }

        [JsonPropertyName("annualMaintenanceTime")]
        public object AnnualMaintenanceTime { get; set; }

        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }

        [JsonPropertyName("municipality")]
        public string Municipality { get; set; }

        [JsonPropertyName("municipalityCode")]
        public int MunicipalityCode { get; set; }

        [JsonPropertyName("province")]
        public string Province { get; set; }

        [JsonPropertyName("provinceCode")]
        public int ProvinceCode { get; set; }

        [JsonPropertyName("direction1Municipality")]
        public string Direction1Municipality { get; set; }

        [JsonPropertyName("direction1MunicipalityCode")]
        public int Direction1MunicipalityCode { get; set; }

        [JsonPropertyName("direction2Municipality")]
        public string Direction2Municipality { get; set; }

        [JsonPropertyName("direction2MunicipalityCode")]
        public int Direction2MunicipalityCode { get; set; }

        [JsonPropertyName("stationType")]
        public string StationType { get; set; }

        [JsonPropertyName("calculatorDeviceType")]
        public object CalculatorDeviceType { get; set; }

        [JsonPropertyName("sensors")]
        public List<int> Sensors { get; set; }

        [JsonPropertyName("freeFlowSpeed1")]
        public double? FreeFlowSpeed1 { get; set; }

        [JsonPropertyName("freeFlowSpeed2")]
        public double? FreeFlowSpeed2 { get; set; }
    }

    public class StationRoadAddress
    {
        [JsonPropertyName("roadNumber")]
        public int RoadNumber { get; set; }

        [JsonPropertyName("roadSection")]
        public int RoadSection { get; set; }

        [JsonPropertyName("distanceFromRoadSectionStart")]
        public int DistanceFromRoadSectionStart { get; set; }

        [JsonPropertyName("carriageway")]
        public string Carriageway { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; }

        [JsonPropertyName("contractArea")]
        public string ContractArea { get; set; }

        [JsonPropertyName("contractAreaCode")]
        public int ContractAreaCode { get; set; }
    }

    public class StationDetailsContract
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("properties")]
        public Properties Properties { get; set; }
    }


}
