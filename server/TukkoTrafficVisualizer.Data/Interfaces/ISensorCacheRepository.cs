using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Interfaces
{
    public interface ISensorCacheRepository : IGenericCacheRepository<Sensor>
    {
    }
}
