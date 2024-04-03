using System.Net;
using System.Net.Http.Json;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class RoadworkHttpService : IRoadworkHttpService
{
    private readonly HttpClient _httpClient;

    public RoadworkHttpService()
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
    }

    public async Task<RoadworkContract> FetchAsync()
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/traffic-message/v1/messages?situationType=ROAD_WORK&includeAreaGeometry=false");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException($"Failed to fetch roadworks: {responseMessage.ReasonPhrase}");
        }

        RoadworkContract? roadworkContract = await responseMessage.Content.ReadFromJsonAsync<RoadworkContract>();

        if (roadworkContract == null)
        {
            throw new InternalServerErrorException("Failed to fetch roadworks");
        }

        return roadworkContract;
    }

}