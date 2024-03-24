using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using Station = TukkoTrafficVisualizer.Data.Entities.Station;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IStationService
    {
        Task<StationContract> FetchStationsAsync();
        Task SaveStationsAsync(StationContract stationContract);
        Task<StationDetailsContract> FetchStationDetailsAsync(int stationId);
        Task<IEnumerable<Station?>> GetAllAsync();
        Task<Station?> GetByIdAsync(string id);
    }
}
