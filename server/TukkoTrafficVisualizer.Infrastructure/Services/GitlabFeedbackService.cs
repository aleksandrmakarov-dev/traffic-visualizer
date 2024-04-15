using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class GitlabFeedbackService:IFeedbackService
    {
        private readonly GitlabOptions _gitlabOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        public GitlabFeedbackService(IOptions<GitlabOptions> gitlabOptions, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _gitlabOptions = gitlabOptions.Value;
        }

        public async Task SendAsync(string title, string description)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_gitlabOptions.BaseUrl);

            HttpRequestMessage requestMessage =
                new HttpRequestMessage(HttpMethod.Post, $"projects/{_gitlabOptions.ProjectId}/issues?title={title}&description={description}&labels=Customer Feedback");

            requestMessage.Headers.Add("Private-Token", _gitlabOptions.AccessToken);

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                string errorMessage = await responseMessage.Content.ReadAsStringAsync();
                throw new BadRequestException(errorMessage);
            }
        }
    }
}
