using System.Net;
using System.Net.Http.Json;
using Server.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Repositories;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using Names = TukkoTrafficVisualizer.Data.Entities.Names;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class StationService : IStationService
{
    private readonly IStationCacheRepository _stationCacheRepository;
    private readonly HttpClient _httpClient;

    public StationService(HttpClient httpClient, IStationCacheRepository stationCacheRepository)
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });
        _stationCacheRepository = stationCacheRepository;
    }

    public async Task<StationContract> FetchStationsAsync()
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

    public async Task SaveStationsAsync(StationContract stationContract)
    {
        await Parallel.ForEachAsync(
            stationContract.Features, 
            new ParallelOptions { MaxDegreeOfParallelism = 20 },
            (async (feature, token) =>
           {
               StationDetailsContract stationDetailsContract = await FetchStationDetailsAsync(feature.Id);

               Data.Entities.Station station = MapStationFeatureToStation(stationDetailsContract);

               await _stationCacheRepository.SetAsync(station);
           }));

    }

    public async Task<StationDetailsContract> FetchStationDetailsAsync(int stationId)
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
    private Data.Entities.Station MapStationFeatureToStation(StationDetailsContract sdc)
    {
        return new Data.Entities.Station
        {
            Id = sdc.Id.ToString(),
            TmsNumber = sdc.Properties.TmsNumber,
            Name = sdc.Properties.Name,
            Names =
            new Names{
                Fi = sdc.Properties.Names ?.Fi ?? string.Empty,
                Sv = sdc.Properties.Names?.Sv ?? sdc.Properties.Names?.Fi ?? string.Empty,
                En = sdc.Properties.Names?.En ?? sdc.Properties.Names?.Fi ?? string.Empty
            },
            Coordinates =
            new Coordinates{
                Longitude = sdc.Geometry.Coordinates[0],
                Latitude = sdc.Geometry.Coordinates[1]
            },
            RoadNumber = sdc.Properties.RoadAddress.RoadNumber,
            RoadSection = sdc.Properties.RoadAddress.RoadSection,
            Carriageway = sdc.Properties.RoadAddress.Carriageway,
            Side = sdc.Properties.RoadAddress.Side,
            Municipality = sdc.Properties.Municipality,
            MunicipalityCode = sdc.Properties.MunicipalityCode,
            Province = sdc.Properties.Province,
            ProvinceCode = sdc.Properties.ProvinceCode,
            Direction1Municipality = sdc.Properties.Direction1Municipality,
            Direction1MunicipalityCode = sdc.Properties.Direction1MunicipalityCode,
            Direction2Municipality = sdc.Properties.Direction2Municipality,
            Direction2MunicipalityCode = sdc.Properties.Direction2MunicipalityCode,
            FreeFlowSpeed1 = sdc.Properties.FreeFlowSpeed1,
            FreeFlowSpeed2 = sdc.Properties.FreeFlowSpeed2,
            DataUpdatedTime = sdc.Properties.DataUpdatedTime
        };
    }
}