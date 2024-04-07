using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly HttpClient _httpClient;
        private readonly GitlabOptions _gitlabOptions;

        public FeedbackController(ILogger<FeedbackController> logger, IOptions<GitlabOptions> gitlabOptions)
        {
            _logger = logger;
            _gitlabOptions = gitlabOptions.Value;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_gitlabOptions.BaseUrl)
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackRequest request)
        {
            _logger.LogInformation($"Title: {request.Title}");
            _logger.LogInformation($"Description: {request.Description}");

            HttpRequestMessage requestMessage =
                new HttpRequestMessage(HttpMethod.Post, $"projects/{_gitlabOptions.ProjectId}/issues?title={request.Title}&description={request.Description}&labels=Customer Feedback");

            requestMessage.Headers.Add("Private-Token",_gitlabOptions.AccessToken);


            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                string errorMessage = await responseMessage.Content.ReadAsStringAsync();
                throw new BadRequestException(errorMessage);
            }

            return Ok(new MessageResponse
            {
                Title = "We received your feedback",
                Message = "Thank you for your feedback. We will carefully look through it"
            });
        }
    }
}
