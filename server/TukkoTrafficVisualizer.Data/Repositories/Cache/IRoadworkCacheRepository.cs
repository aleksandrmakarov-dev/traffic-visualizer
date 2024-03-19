using System.Linq.Expressions;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Cache
{
    public interface IRoadworkCacheRepository : IGenericCacheRepository<Roadwork>
    {
        Task<IEnumerable<Roadwork>> GetAsync(string severity);
    }
}
