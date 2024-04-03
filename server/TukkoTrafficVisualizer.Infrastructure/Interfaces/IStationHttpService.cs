using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IStationHttpService
    {
        Task<StationContract> FetchAsync();
        Task<StationDetailsContract> FetchDetailsAsync(int stationId);
    }
}
