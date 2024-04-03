﻿
namespace TukkoTrafficVisualizer.Database.Entities
{
    public class Roadwork : Entity
    {
        public int? PrimaryPointRoadNumber { get; set; }
        public int? PrimaryPointRoadSection { get; set; }
        public int? SecondaryPointRoadNumber { get; set; }
        public int? SecondaryPointRoadSection { get; set; }
        public string? Direction { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Severity { get; set; }

        public IEnumerable<WorkType> WorkTypes { get; set; } = new List<WorkType>();
        public IEnumerable<WorkingHours> WorkingHours { get; set; } = new List<WorkingHours>();
        public IEnumerable<Restriction> Restrictions { get; set; } = new List<Restriction>();
    }

    public class WorkType
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class WorkingHours
    {
        public string Weekday { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class Restriction
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}

