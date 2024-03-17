using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public interface ISensorService
{
    Task<SensorContract> FetchSensorsAsync();
    Task SaveSensorsAsync(SensorContract sensorContract);
    Task<IEnumerable<Sensor>> GetAsync();
}