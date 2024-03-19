namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IMailingService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
