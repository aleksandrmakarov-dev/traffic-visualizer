using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
