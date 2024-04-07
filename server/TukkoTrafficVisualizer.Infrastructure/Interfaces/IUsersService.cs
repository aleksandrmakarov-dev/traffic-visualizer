using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<bool> AddFavoriteStationsAsync(string id, string stationId);
        Task<IEnumerable<string>> GetFavoriteStationsAsync(string id);
        Task<bool> RemoveFavoriteStationAsync(string id, string stationId);
    }
}
