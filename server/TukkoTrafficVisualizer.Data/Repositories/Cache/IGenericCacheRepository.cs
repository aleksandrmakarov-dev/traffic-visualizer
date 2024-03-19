using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Cache;

public interface IGenericCacheRepository<in T> where T : Entity
{
    Task<bool> CreateIndexAsync();
    Task<string?> SetAsync(T model, TimeSpan? expireSpan = null);
    Task<bool> DeleteAsync(int id);
}