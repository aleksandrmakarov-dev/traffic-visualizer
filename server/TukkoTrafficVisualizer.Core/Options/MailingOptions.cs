namespace TukkoTrafficVisualizer.Core.Options
{
    public class MailingOptions:IOptionsBase
    {
        public required string Smtp { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int Tsl { get; set; }
        public int Ssl { get; set; }
        public static string Name => "Mailing";
    }
}
