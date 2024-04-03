using System.Net;
using System.Net.Http.Json;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class StationHttpService : IStationHttpService
{
    private readonly HttpClient _httpClient;

    public StationHttpService()
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
    }

    public async Task<StationContract> FetchAsync()
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/tms/v1/stations");

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
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/tms/v1/stations/{stationId}");

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