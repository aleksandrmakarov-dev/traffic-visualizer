using System.Linq.Expressions;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Interfaces;

public interface IGenericCacheRepository<T> where T : Entity
{
    Task<bool> CreateIndexAsync();
    Task<string?> SetAsync(T? model, TimeSpan? expireSpan = null);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where);
    Task<T?> GetByIdAsync(string id);
}