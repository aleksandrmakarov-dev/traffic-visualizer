using Redis.OM;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Interfaces;

namespace TukkoTrafficVisualizer.Data.Repositories
{
    public interface IStationCacheRepository : IGenericCacheRepository<Station>
    {

    }
    public class StationCacheRepository : GenericCacheRepository<Station>, IStationCacheRepository
    {
        public StationCacheRepository(RedisConnectionProvider provider) : base(provider)
        {

        }
    }
}
