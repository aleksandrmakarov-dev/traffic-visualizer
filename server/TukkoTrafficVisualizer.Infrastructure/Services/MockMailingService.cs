using Microsoft.Extensions.Options;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class MockMailingService:IMailingService
    {
        private readonly MailingOptions _options;

        public MockMailingService(IOptions<MailingOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            Console.WriteLine($"From: {_options.From}");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");

            await Task.FromResult(true);
        }
    }
}
