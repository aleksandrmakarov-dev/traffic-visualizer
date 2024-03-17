using System.Linq.Expressions;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories
{
    public interface IRoadworkCacheRepository:IGenericCacheRepository<Roadwork>
    {
        Task<IEnumerable<Roadwork>> GetAsync(string severity);
    }
}
