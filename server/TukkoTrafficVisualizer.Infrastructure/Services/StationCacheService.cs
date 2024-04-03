using TukkoTrafficVisualizer.Cache.Entities;
using TukkoTrafficVisualizer.Cache.Interfaces;
using TukkoTrafficVisualizer.Database.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class StationCacheService : IStationCacheService
{
    private readonly IStationCacheRepository _stationCacheRepository;
    private readonly IStationHttpService _stationHttpService;

    public StationCacheService(IStationCacheRepository stationCacheRepository, IStationRepository stationRepository, IStationHttpService stationHttpService)
    {
        _stationCacheRepository = stationCacheRepository;
        _stationHttpService = stationHttpService;
    }

    public async Task<IEnumerable<Cache.Entities.Station>> GetCacheAllAsync()
    {
        return await _stationCacheRepository.GetAllAsync();
    }

    public async Task<Cache.Entities.Station?> GetCacheByIdAsync(string id)
    {
        return await _stationCacheRepository.GetByIdAsync(id);
    }

    public async Task SaveStationsAsync(StationContract stationContract)
    {
        await Parallel.ForEachAsync(
            stationContract.Features,
            new ParallelOptions { MaxDegreeOfParallelism = 20 },
            async (feature, token) =>
            {
                StationDetailsContract stationDetailsContract = await _stationHttpService.FetchDetailsAsync(feature.Id);

                Cache.Entities.Station station = MapStationFeatureToStation(stationDetailsContract);

                await _stationCacheRepository.SetAsync(station,TimeSpan.FromDays(30));
            });
    }

    private Cache.Entities.Station MapStationFeatureToStation(StationDetailsContract sdc)
    {
        return new Cache.Entities.Station
        {
            Id = sdc.Id.ToString(),
            TmsNumber = sdc.Properties.TmsNumber,
            Name = sdc.Properties.Name,
            Names =
            new Cache.Entities.Names
            {
                Fi = sdc.Properties.Names?.Fi ?? string.Empty,
                Sv = sdc.Properties.Names?.Sv ?? sdc.Properties.Names?.Fi ?? string.Empty,
                En = sdc.Properties.Names?.En ?? sdc.Properties.Names?.Fi ?? string.Empty
            },
            Coordinates =
            new Coordinates
            {
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