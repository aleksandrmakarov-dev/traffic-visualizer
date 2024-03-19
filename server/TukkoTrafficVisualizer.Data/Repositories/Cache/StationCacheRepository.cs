using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redis.OM;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Cache
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
