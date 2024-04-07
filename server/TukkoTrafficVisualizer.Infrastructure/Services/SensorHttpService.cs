using System.Net;
using System.Net.Http.Json;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class SensorHttpService : ISensorHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SensorHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SensorContract> FetchAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(Core.Constants.Constants.DigitrafficHttpClientName);

        HttpResponseMessage responseMessage = await httpClient.GetAsync($"tms/v1/stations/data");

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