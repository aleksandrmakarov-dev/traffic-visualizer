using System.Net;
using System.Net.Http.Json;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class SensorHttpService : ISensorHttpService
{
    private readonly HttpClient _httpClient;

    public SensorHttpService()
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
    }

    public async Task<SensorContract> FetchAsync()
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/tms/v1/stations/data");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException($"Failed to fetch sensors: {responseMessage.ReasonPhrase}");
        }

        SensorContract? sensorContract = await responseMessage.Content.ReadFromJsonAsync<SensorContract>();

        if (sensorContract == null)
        {
            throw new InternalBufferOverflowException("Failed to fetch sensors");
        }

        return sensorContract;
    }
}