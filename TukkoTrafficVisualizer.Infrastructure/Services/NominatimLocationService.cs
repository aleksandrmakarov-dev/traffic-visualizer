﻿using System.Net.Http.Json;
using Server.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class NominatimLocationService:ILocationService
    {
        private readonly HttpClient _httpClient;

        public NominatimLocationService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient(nameof(NominatimLocationService));
        }

        public async Task<IEnumerable<LocationResponse>> FindByQueryAsync(string query)
        {
            // https://nominatim.openstreetmap.org/search.php?q=tes&format=jsonv2
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"search.php?q={query}&format=jsonv2");

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
