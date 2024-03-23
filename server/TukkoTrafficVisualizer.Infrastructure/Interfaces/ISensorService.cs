using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces;

public interface ISensorService
{
    Task<SensorContract> FetchSensorsAsync();
    Task SaveSensorsAsync(SensorContract sensorContract);
    Task<IEnumerable<Sensor>> GetAsync(string[]? ids = null);
}