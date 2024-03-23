using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Interfaces;

namespace TukkoTrafficVisualizer.Data.Repositories
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
