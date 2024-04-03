using Redis.OM;
using TukkoTrafficVisualizer.Cache.Entities;
using TukkoTrafficVisualizer.Cache.Interfaces;

namespace TukkoTrafficVisualizer.Cache.Repositories
{
    public class StationCacheRepository : GenericCacheRepository<Station>, IStationCacheRepository
    {
        public StationCacheRepository(RedisConnectionProvider provider) : base(provider)
        {

        }
    }
}
