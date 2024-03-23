using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Interfaces
{
    public interface ISessionsRepository : IGenericRepository<Session>
    {
        Task<Session?> GetByRefreshTokenAsync(string refreshToken);
    }
}
