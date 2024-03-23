using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IStationService
    {
        Task<StationContract> FetchStationsAsync();
        Task SaveStationsAsync(StationContract stationContract);
        Task<StationDetailsContract> FetchStationDetailsAsync(int stationId);
        Task<IEnumerable<Data.Entities.Station>> GetAllAsync();
    }
}
