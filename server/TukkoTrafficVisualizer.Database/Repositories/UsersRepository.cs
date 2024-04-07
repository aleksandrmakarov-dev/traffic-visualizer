using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;

namespace TukkoTrafficVisualizer.Database.Repositories;

public class UsersRepository:GenericRepository<User>,IUsersRepository
{
    public UsersRepository(IMongoClient client, IClientSessionHandle session) : base(client, session)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await Collection.AsQueryable().FirstOrDefaultAsync(e => e.Email == email);
    }
}