using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TukkoTrafficVisualizer.Infrastructure.Models.Contracts
{
    public class RoadworkContract
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }

        [JsonPropertyName("features")]
        public List<Feature> Features { get; set; }
    }

    public class AlertCLocation
    {
        [JsonPropertyName("locationCode")]
        public int LocationCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }

    public class Announcement
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("locationDetails")]
        public LocationDetails LocationDetails { get; set; }

        [JsonPropertyName("features")]
        public List<object> Features { get; set; }

        [JsonPropertyName("roadWorkPhases")]
        public List<RoadWorkPhase> RoadWorkPhases { get; set; }

        [JsonPropertyName("timeAndDuration")]
        public TimeAndDuration TimeAndDuration { get; set; }

        [JsonPropertyName("additionalInformation")]
        public string AdditionalInformation { get; set; }

        [JsonPropertyName("sender")]
        public string Sender { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }

    public class Contact
    {
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class Feature
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("properties")]
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<object> Coordinates { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("countryCode")]
        public int CountryCode { get; set; }

        [JsonPropertyName("locationTableNumber")]
        public int LocationTableNumber { get; set; }

        [JsonPropertyName("locationTableVersion")]
        public string LocationTableVersion { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class LocationDetails
    {
        [JsonPropertyName("roadAddressLocation")]
        public RoadAddressLocation RoadAddressLocation { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("situationId")]
        public string SituationId { get; set; }

        [JsonPropertyName("situationType")]
        public string SituationType { get; set; }

        [JsonPropertyName("trafficAnnouncementType")]
        public object TrafficAnnouncementType { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("releaseTime")]
        public DateTime ReleaseTime { get; set; }

        [JsonPropertyName("versionTime")]
        public DateTime VersionTime { get; set; }

        [JsonPropertyName("announcements")]
        public List<Announcement> Announcements { get; set; }

        [JsonPropertyName("contact")]
        public Contact Contact { get; set; }

        [JsonPropertyName("dataUpdatedTime")]
        public DateTime DataUpdatedTime { get; set; }
    }

    public class RoadAddress
    {
        [JsonPropertyName("road")]
        public int Road { get; set; }

        [JsonPropertyName("roadSection")]
        public int RoadSection { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }

    public class RoadAddressLocation
    {
        [JsonPropertyName("primaryPoint")]
        public RoadPoint? PrimaryPoint { get; set; }

        [JsonPropertyName("secondaryPoint")]
        public RoadPoint? SecondaryPoint { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("directionDescription")]
        public string DirectionDescription { get; set; }
    }

    public class RoadWorkPhase
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("locationDetails")]
        public LocationDetails LocationDetails { get; set; }

        [JsonPropertyName("workingHours")]
        public List<WorkingHour> WorkingHours { get; set; }

        [JsonPropertyName("timeAndDuration")]
        public TimeAndDuration TimeAndDuration { get; set; }

        [JsonPropertyName("workTypes")]
        public List<WorkType> WorkTypes { get; set; }

        [JsonPropertyName("restrictionsLiftable")]
        public bool RestrictionsLiftable { get; set; }

        [JsonPropertyName("severity")]
        public string Severity { get; set; }

        [JsonPropertyName("slowTrafficTimes")]
        public List<object> SlowTrafficTimes { get; set; }

        [JsonPropertyName("queuingTrafficTimes")]
        public List<object> QueuingTrafficTimes { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("restrictions")]
        public List<Restriction> Restrictions { get; set; }
    }

    public class RoadPoint
    {
        [JsonPropertyName("municipality")]
        public string Municipality { get; set; }

        [JsonPropertyName("province")]
        public string Province { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("roadAddress")]
        public RoadAddress RoadAddress { get; set; }

        [JsonPropertyName("alertCLocation")]
        public AlertCLocation AlertCLocation { get; set; }

        [JsonPropertyName("roadName")]
        public string RoadName { get; set; }
    }

    public class TimeAndDuration
    {
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
    }

    public class WorkingHour
    {
        [JsonPropertyName("weekday")]
        public string Weekday { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }
    }

    public class WorkType
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Restriction
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("restriction")]
        public RestrictionData RestrictionData { get; set; }
    }

    public class RestrictionData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}
