using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class MockFeedbackService:IFeedbackService
    {
        private readonly ILogger<MockFeedbackService> _logger;

        public MockFeedbackService(ILogger<MockFeedbackService> logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(string title, string description)
        {
            _logger.LogInformation($"=== Feedback ===\nTitle: {title}\nDescription: {description}");

            await Task.Delay(1);
        }
    }
}
