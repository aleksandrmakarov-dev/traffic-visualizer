namespace TukkoTrafficVisualizer.Core.Constants
{
    public static class Constants
    {
        public const string UserContextName = "user";
        public const bool CookieHttpOnly = true;
        public const string DigiTrafficHttpClientName = "digitraffic";
        public const string NominatimHttpClientName = "nominatim";
        public const string GitlabHttpClientName = "gitlab";
    }

    public enum TimeRange
    {
        Today,
        Yesterday,
        Week,
        Month
    }
}
