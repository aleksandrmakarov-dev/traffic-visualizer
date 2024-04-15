

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IFeedbackService
    {
        Task SendAsync(string title, string description);
    }
}
