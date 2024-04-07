using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class StationService:IStationService
    {
        private readonly IStationRepository _stationRepository;
        private readonly IStationHttpService _stationHttpService;

        public StationService(IStationRepository stationRepository, IStationHttpService stationHttpService)
        {
            _stationRepository = stationRepository;
            _stationHttpService = stationHttpService;
        }

        public async Task SaveAsync(StationContract stationContract)
        {
            await Parallel.ForEachAsync(
                stationContract.Features,
                new ParallelOptions { MaxDegreeOfParallelism = 20 },
                async (feature, token) =>
                {
                    StationDetailsContract stationDetailsContract = await _stationHttpService.FetchDetailsAsync(feature.Id);

                    Database.Entities.Station station = MapStationFeatureToStation(stationDetailsContract);

                    await _stationRepository.ReplaceByIdAsync(station, new ReplaceOptions { IsUpsert = true });
                });
        }

        public async Task<Database.Entities.Station?> GetHistoryByIdAsync(string id)
        {
            return await _stationRepository.GetByStationIdWithSensorsAsync(id);
        }

        private Database.Entities.Station MapStationFeatureToStation(StationDetailsContract sdc)
        {
            return new Database.Entities.Station
            {
                StationId = sdc.Id.ToString(),
                TmsNumber = sdc.Properties.TmsNumber,
                Name = sdc.Properties.Name,
                Coordinates =
                    new Database.Entities.Coordinates
                    {
                        Longitude = sdc.Geometry.Coordinates[0],
                        Latitude = sdc.Geometry.Coordinates[1]
                    },
                DataUpdatedTime = sdc.Properties.DataUpdatedTime
            };
        }
    }
}
