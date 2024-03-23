using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Interfaces;

namespace TukkoTrafficVisualizer.Data.Repositories;

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