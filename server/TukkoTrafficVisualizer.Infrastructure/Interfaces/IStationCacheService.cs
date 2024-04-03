using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IStationCacheService
    {
        Task SaveStationsAsync(StationContract stationContract);
        Task<IEnumerable<Cache.Entities.Station>> GetCacheAllAsync();
        Task<Cache.Entities.Station?> GetCacheByIdAsync(string id);
    }
}
