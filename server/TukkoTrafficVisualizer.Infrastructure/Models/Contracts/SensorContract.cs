using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Contracts
{
    public class SensorContract
    {
        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }

        [JsonPropertyName("stations")]
        public List<Station> Stations { get; set; }
    }

    public class SensorValue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("stationId")]
        public int StationId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("timeWindowStart")]
        public DateTime? TimeWindowStart { get; set; }

        [JsonPropertyName("timeWindowEnd")]
        public DateTime? TimeWindowEnd { get; set; }

        [JsonPropertyName("measuredTime")]
        public DateTime? MeasuredTime { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class Station
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tmsNumber")]
        public int TmsNumber { get; set; }

        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }

        [JsonPropertyName("sensorValues")]
        public List<SensorValue> SensorValues { get; set; }
    }
}
