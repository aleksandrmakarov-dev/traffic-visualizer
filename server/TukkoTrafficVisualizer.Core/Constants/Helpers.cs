namespace TukkoTrafficVisualizer.Core.Constants;

public static class Helpers
{
    public static Tuple<DateTime, DateTime> ConvertTimeRangeToDateTime(TimeRange timeRange)
    {
        DateTime start;
        DateTime end;

        switch (timeRange)
        {
            case TimeRange.Today:
                start = DateTime.UtcNow.Date;
                end = start.AddDays(1).AddSeconds(-1);
                break;
            case TimeRange.Yesterday:
                start = DateTime.UtcNow.Date.AddDays(-1);
                end = start.AddDays(1).AddSeconds(-1);
                break;
            case TimeRange.Week:
                start = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
                end = start.AddDays(7).AddSeconds(-1);
                break;
            default:
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
                break;
        }

        return new Tuple<DateTime, DateTime>(start, end);
    }
}