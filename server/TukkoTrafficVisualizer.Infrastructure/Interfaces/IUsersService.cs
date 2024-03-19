using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserResponse>> GetAllAsync();
    }
}
