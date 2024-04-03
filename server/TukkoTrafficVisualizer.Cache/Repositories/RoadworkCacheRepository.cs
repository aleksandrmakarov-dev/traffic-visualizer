using Redis.OM;
using TukkoTrafficVisualizer.Cache.Entities;
using TukkoTrafficVisualizer.Cache.Interfaces;


namespace TukkoTrafficVisualizer.Cache.Repositories;

public class RoadworkCacheRepository : GenericCacheRepository<Roadwork>, IRoadworkCacheRepository
{
    public RoadworkCacheRepository(RedisConnectionProvider provider) : base(provider)
    {
    }

    public async Task<IEnumerable<Roadwork>> GetAsync(string severity)
    {
        return await Collection
            .Where(r => r.Severity == severity)
            .ToListAsync();
    }
}