using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Contracts
{
    public class StationFeature
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("properties")]
        public StationProperties Properties { get; set; }
    }


    public class StationProperties
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
        public string State { get; set; }

        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }
    }

    public class StationContract
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }

        [JsonPropertyName("features")]
        public List<StationFeature> Features { get; set; }
    }
}
