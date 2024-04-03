using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces;

public interface IStationService
{
    Task SaveStationsAsync(StationContract stationContract);
}