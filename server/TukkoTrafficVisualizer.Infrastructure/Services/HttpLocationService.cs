using System.Net.Http.Json;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class HttpLocationService : ILocationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpLocationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<LocationResponse>> FindByQueryAsync(string query)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient(Core.Constants.Constants.NominatimHttpClientName);

            HttpResponseMessage responseMessage = await httpClient.GetAsync($"/search.php?q={query}&format=jsonv2");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new BadRequestException($"Failed to get locations: {responseMessage.ReasonPhrase}");
            }

            IEnumerable<LocationResponse>? locations = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<LocationResponse>>();

            return (locations ?? Array.Empty<LocationResponse>())
                .OrderBy(l => l.PlaceRank)
                .ThenByDescending(l => l.Importance);
        }
    }
}
