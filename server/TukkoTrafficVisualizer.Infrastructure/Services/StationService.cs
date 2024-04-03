using System;
using System.Collections.Generic;
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

        public async Task SaveStationsAsync(StationContract stationContract)
        {
            await Parallel.ForEachAsync(
                stationContract.Features,
                new ParallelOptions { MaxDegreeOfParallelism = 20 },
                async (feature, token) =>
                {
                    StationDetailsContract stationDetailsContract = await _stationHttpService.FetchDetailsAsync(feature.Id);

                    Database.Entities.Station station = MapStationFeatureToStation(stationDetailsContract);

                    await _stationRepository.UpdateByStationIdAsync(station, new ReplaceOptions { IsUpsert = true });
                });
        }

        private Database.Entities.Station MapStationFeatureToStation(StationDetailsContract sdc)
        {
            return new Database.Entities.Station
            {
                StationId = sdc.Id.ToString(),
                TmsNumber = sdc.Properties.TmsNumber,
                Name = sdc.Properties.Name,
                Names =
                    new Database.Entities.Names
                    {
                        Fi = sdc.Properties.Names?.Fi ?? string.Empty,
                        Sv = sdc.Properties.Names?.Sv ?? sdc.Properties.Names?.Fi ?? string.Empty,
                        En = sdc.Properties.Names?.En ?? sdc.Properties.Names?.Fi ?? string.Empty
                    },
                Coordinates =
                    new Database.Entities.Coordinates
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
}
