using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;

namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface IStationRepository:IGenericRepository<Station>
    {
        Task<Station> UpdateByStationIdAsync(Station station, ReplaceOptions options);
    }
}
