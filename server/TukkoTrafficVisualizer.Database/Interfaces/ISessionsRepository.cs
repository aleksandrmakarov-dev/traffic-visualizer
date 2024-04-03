using TukkoTrafficVisualizer.Database.Entities;

namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface ISessionsRepository : IGenericRepository<Session>
    {
        Task<Session?> GetByRefreshTokenAsync(string refreshToken);
    }
}
