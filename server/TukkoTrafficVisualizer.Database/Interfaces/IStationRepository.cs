using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;

namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface IStationRepository:IGenericRepository<Station>
    {
        Task ReplaceByIdAsync(Station station, ReplaceOptions options);
        Task<Station?> GetByStationIdAsync(string stationId);
        Task<Station?> GetByStationIdWithSensorsAsync(string id);
    }
}
