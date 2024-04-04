using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;

namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface ISensorRepository:IGenericRepository<Sensor>
    {
        Task UpdateBySensorIdAsync(Sensor sensor, ReplaceOptions options);
    }
}
