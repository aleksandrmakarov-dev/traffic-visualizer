using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class MockMailingService : IMailingService
    {
        private readonly MailingOptions _options;
        private readonly ILogger<MockMailingService> _logger;

        public MockMailingService(IOptions<MailingOptions> options, ILogger<MockMailingService> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            _logger.LogInformation($"From: {_options.From}\nTo: {to}\nSubject: {subject}\nBody: {body}");

            await Task.FromResult(true);
        }
    }
}
