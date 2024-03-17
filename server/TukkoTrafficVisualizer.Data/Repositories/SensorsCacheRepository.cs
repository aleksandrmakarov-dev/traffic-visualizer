using Redis.OM;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories;

public class SensorsCacheRepository : GenericCacheRepository<Sensor>,ISensorCacheRepository
{
    public SensorsCacheRepository(RedisConnectionProvider provider) : base(provider)
    {
    }


    public async Task<IEnumerable<Sensor>> GetAsync()
    {
        return await Collection.ToListAsync();
    }
}