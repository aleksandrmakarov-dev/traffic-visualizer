using Redis.OM;
using TukkoTrafficVisualizer.Cache.Entities;
using TukkoTrafficVisualizer.Cache.Interfaces;

namespace TukkoTrafficVisualizer.Cache.Repositories;

public class SensorsCacheRepository : GenericCacheRepository<Sensor>, ISensorCacheRepository
{
    public SensorsCacheRepository(RedisConnectionProvider provider) : base(provider)
    {
    }
}