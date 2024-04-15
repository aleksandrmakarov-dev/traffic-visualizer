using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(ILogger<FeedbackController> logger, IOptions<GitlabOptions> gitlabOptions, IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackRequest request)
        {
            await _feedbackService.SendAsync(request.Title, request.Description);

            return Ok(new MessageResponse
            {
                Title = "We received your feedback",
                Message = "Thank you for your feedback. We will carefully look through it"
            });
        }
    }
}
