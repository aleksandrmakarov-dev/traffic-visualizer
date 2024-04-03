using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces;

public interface IRoadworkHttpService
{
    Task<RoadworkContract> FetchAsync();
}