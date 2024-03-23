using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Interfaces
{
    public interface IRoadworkCacheRepository : IGenericCacheRepository<Roadwork>
    {
        Task<IEnumerable<Roadwork>> GetAsync(string severity);
    }
}
