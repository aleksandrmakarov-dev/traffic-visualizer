using Redis.OM;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Interfaces;

namespace TukkoTrafficVisualizer.Data.Repositories;

public class SensorsCacheRepository : GenericCacheRepository<Sensor>, ISensorCacheRepository
{
    public SensorsCacheRepository(RedisConnectionProvider provider) : base(provider)
    {
    }
}