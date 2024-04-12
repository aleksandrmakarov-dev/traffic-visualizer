using TukkoTrafficVisualizer.Cache.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IRoadworkCacheService
    {
        Task SaveRoadworksAsync(RoadworkContract roadworkContract);
        Task<IEnumerable<Roadwork>> GetAsync();
    }
}
