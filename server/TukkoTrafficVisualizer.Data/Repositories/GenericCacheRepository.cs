using System.Linq.Expressions;
using Redis.OM;
using Redis.OM.Searching;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Interfaces;

namespace TukkoTrafficVisualizer.Data.Repositories;

public class GenericCacheRepository<T> : IGenericCacheRepository<T> where T : Entity
{
    protected readonly RedisConnectionProvider Provider;
    protected readonly IRedisCollection<T> Collection;

    public GenericCacheRepository(RedisConnectionProvider provider)
    {
        Provider = provider;
        Collection = provider.RedisCollection<T>();
    }
    public virtual async Task<bool> CreateIndexAsync()
    {
        return await Provider.Connection.CreateIndexAsync(typeof(T));
    }

    public virtual async Task<string?> SetAsync(T model, TimeSpan? expireSpan = null)
    {
        return await Collection.InsertAsync(model, WhenKey.Always, expireSpan);
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        T? model = await Collection.FindByIdAsync(id.ToString());

        if (model == null)
        {
            return false;
        }

        await Collection.DeleteAsync(model);

        return true;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Collection.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
    {
        return await Collection.Where(where).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await Collection.FindByIdAsync(id);
    }
}