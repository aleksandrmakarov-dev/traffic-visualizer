using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Database
{
    public interface ISessionsRepository:IGenericRepository<Session>
    {
        Task<Session?> GetByRefreshTokenAsync(string refreshToken);
    }
}
