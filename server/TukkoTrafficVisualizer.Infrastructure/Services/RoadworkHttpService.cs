using System.Net;
using System.Net.Http.Json;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class RoadworkHttpService : IRoadworkHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RoadworkHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<RoadworkContract> FetchAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(Core.Constants.Constants.DigitrafficHttpClientName);

        HttpResponseMessage responseMessage = await httpClient.GetAsync($"traffic-message/v1/messages?situationType=ROAD_WORK&includeAreaGeometry=false");

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