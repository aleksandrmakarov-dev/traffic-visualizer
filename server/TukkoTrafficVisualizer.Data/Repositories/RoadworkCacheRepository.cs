using System.Linq.Expressions;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Literals.Enums;
using Redis.OM;
using Redis.OM.Searching;
using StackExchange.Redis;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories;

public class RoadworkCacheRepository :GenericCacheRepository<Roadwork>, IRoadworkCacheRepository
{
    public RoadworkCacheRepository(RedisConnectionProvider provider) : base(provider)
    {
    }

    public async Task<IEnumerable<Roadwork>> GetAsync(string severity)
    {
        return await Collection
            .Where(r=>r.Severity == severity)
            .ToListAsync();
    }
}