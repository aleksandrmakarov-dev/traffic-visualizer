using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Repositories.Database
{
    public interface IUsersRepository: IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email); 
    }
}
