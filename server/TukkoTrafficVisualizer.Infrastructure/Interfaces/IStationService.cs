using TukkoTrafficVisualizer.Core.Constants;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using Station = TukkoTrafficVisualizer.Database.Entities.Station;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces;

public interface IStationService
{
    Task SaveAsync(StationContract stationContract);
    Task<Station?> GetHistoryByIdAsync(string id, TimeRange timeRange);
}