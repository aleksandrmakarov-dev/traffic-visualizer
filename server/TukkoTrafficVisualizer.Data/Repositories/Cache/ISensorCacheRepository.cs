using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Cache
{
    public interface ISensorCacheRepository : IGenericCacheRepository<Sensor>
    {
        Task<IEnumerable<Sensor>> GetAsync();
    }
}
