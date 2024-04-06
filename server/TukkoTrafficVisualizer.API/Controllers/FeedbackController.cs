using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TukkoTrafficVisualizer.API.Hubs;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FeedbackController(ILogger<FeedbackController> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
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

        [HttpPost("send")]
        public async Task<IActionResult> Post([FromBody] string data)
        {
            await _hubContext.Clients.All.SendAsync("Notification", data);
            return Ok();
        }
    }
}
