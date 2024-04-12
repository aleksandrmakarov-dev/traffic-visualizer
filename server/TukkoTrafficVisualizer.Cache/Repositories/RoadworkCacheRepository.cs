using Redis.OM;
using TukkoTrafficVisualizer.Cache.Entities;
using TukkoTrafficVisualizer.Cache.Interfaces;


namespace TukkoTrafficVisualizer.Cache.Repositories;

public class RoadworkCacheRepository : GenericCacheRepository<Roadwork>, IRoadworkCacheRepository
{
    public RoadworkCacheRepository(RedisConnectionProvider provider) : base(provider)
    {
    }
}