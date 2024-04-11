
using System.Net.Http.Json;
using Microsoft.VisualBasic;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class StationHttpService : IStationHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StationHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<StationContract> FetchAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(Core.Constants.Constants.DigiTrafficHttpClientName);

        HttpResponseMessage responseMessage = await httpClient.GetAsync($"tms/v1/stations");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException($"Failed to fetch stations: {responseMessage.ReasonPhrase}");
        }

        StationContract? stationContract = await responseMessage.Content.ReadFromJsonAsync<StationContract>();

        if (stationContract == null)
        {
            throw new InternalServerErrorException("Failed to fetch stations");
        }

        return stationContract;
    }

    public async Task<StationDetailsContract> FetchDetailsAsync(int stationId)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(Core.Constants.Constants.DigiTrafficHttpClientName);

        HttpResponseMessage responseMessage = await httpClient.GetAsync($"tms/v1/stations/{stationId}");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException($"Failed to fetch station details: {responseMessage.ReasonPhrase}");
        }

        StationDetailsContract? stationDetailsContract = await responseMessage.Content.ReadFromJsonAsync<StationDetailsContract>();

        if (stationDetailsContract == null)
        {
            throw new InternalServerErrorException("Failed to fetch stations");
        }

        return stationDetailsContract;
    }
}