using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;

namespace TukkoTrafficVisualizer.Database.Repositories
{
    public class SessionsRepository:GenericRepository<Session>, ISessionsRepository
    {
        public SessionsRepository(IMongoClient client, IClientSessionHandle session) : base(client, session)
        {
        }

        public async Task<Session?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await Collection.AsQueryable().FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        }
    }
}
