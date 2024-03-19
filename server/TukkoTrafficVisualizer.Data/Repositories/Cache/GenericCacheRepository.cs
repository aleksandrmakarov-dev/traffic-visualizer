using Redis.OM;
using Redis.OM.Searching;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Cache;

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

    public virtual async Task<bool> DeleteAsync(int id)
    {
        T? model = await Collection.FindByIdAsync(id.ToString());

        if (model == null)
        {
            return false;
        }

        await Collection.DeleteAsync(model);

        return true;
    }
}