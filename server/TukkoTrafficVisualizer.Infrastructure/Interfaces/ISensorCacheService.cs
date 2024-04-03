using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces;

public interface ISensorCacheService
{
    Task SaveSensorsAsync(SensorContract sensorContract);
    Task<IEnumerable<Cache.Entities.Sensor>> GetAsync(string[]? ids = null, string? stationId = null);
}