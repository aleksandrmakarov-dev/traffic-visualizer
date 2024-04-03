using TukkoTrafficVisualizer.Database.Entities;


namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
