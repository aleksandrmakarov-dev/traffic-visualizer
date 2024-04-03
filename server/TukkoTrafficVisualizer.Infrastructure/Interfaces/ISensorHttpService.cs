using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces;

public interface ISensorHttpService
{
    Task<SensorContract> FetchAsync();
}