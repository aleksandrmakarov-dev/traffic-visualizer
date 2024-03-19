using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using Station = TukkoTrafficVisualizer.Infrastructure.Models.Contracts.Station;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IStationService
    {
        Task<StationContract> FetchStationsAsync();
        Task SaveStationsAsync(StationContract stationContract);
        Task<StationDetailsContract> FetchStationDetailsAsync(int stationId);
    }
}
