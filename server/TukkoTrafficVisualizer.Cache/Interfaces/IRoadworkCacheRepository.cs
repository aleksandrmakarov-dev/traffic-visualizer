using TukkoTrafficVisualizer.Cache.Entities;

namespace TukkoTrafficVisualizer.Cache.Interfaces
{
    public interface IRoadworkCacheRepository : IGenericCacheRepository<Roadwork>
    {
        Task<IEnumerable<Roadwork>> GetAsync(string severity);
    }
}
