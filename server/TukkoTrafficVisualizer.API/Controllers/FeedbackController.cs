using Microsoft.AspNetCore.Mvc;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(ILogger<FeedbackController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackRequest request)
        {
            _logger.LogInformation($"Title: {request.Title}");
            _logger.LogInformation($"Description: {request.Description}");

            await Task.Delay(1000);

            return Ok(new MessageResponse
            {
                Title = "We received your feedback",
                Message = "Thank you for your feedback. We will carefully look through it"
            });
        }
    }
}
