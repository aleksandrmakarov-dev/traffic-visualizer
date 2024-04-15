using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class MockMailingService:IMailingService
    {
        private readonly ILogger<MockMailingService> _logger;

        public MockMailingService(ILogger<MockMailingService> logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(string to, string subject, string html)
        {
            _logger.LogInformation($"=== Email Verification Token ===\nTo: {to}\nSubject: {subject}\nHtml: {html}");

            await Task.Delay(1);
        }
    }
}
