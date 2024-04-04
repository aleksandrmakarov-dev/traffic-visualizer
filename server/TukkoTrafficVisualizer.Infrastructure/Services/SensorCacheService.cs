using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using System.Linq.Expressions;
using LinqKit;
using TukkoTrafficVisualizer.Cache.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class SensorCacheService : ISensorCacheService
    {
        private readonly ISensorCacheRepository _sensorCacheRepository;
        private readonly int[] _fastExpireSensorIds = [5058, 5061, 5116, 5119, 5122, 5125, 5158, 5161, 5164, 5168];
        private readonly int[] _slowExpireSensorIds = [5054, 5055, 5056, 5057];
        private readonly int[] _ignoreSensorIds = [5016, 5019, 5022, 5025, 5064, 5067, 5068, 5071, 5152];

        public SensorCacheService(ISensorCacheRepository sensorCacheRepository)
        {
            _sensorCacheRepository = sensorCacheRepository;
        }

        public async Task SaveSensorsAsync(SensorContract sensorContract)
        {
            foreach (Models.Contracts.Station station in sensorContract.Stations)
            {
                // ignore sensors
                IEnumerable<SensorValue> sensorValues =
                    station.SensorValues.Where(sv => !_ignoreSensorIds.Contains(sv.Id));

                foreach (SensorValue sensorValue in sensorValues)
                {
                    Cache.Entities.Sensor sensor = MapSensorValueToSensor(sensorValue);

                    TimeSpan expireSpan = GetExpireSpan(sensorValue);

                    if (expireSpan > TimeSpan.Zero)
                    {
                        await _sensorCacheRepository.SetAsync(sensor, expireSpan);
                    }
                }
            }
        }

        public async Task<IEnumerable<Cache.Entities.Sensor>> GetAsync(string[]? ids = null, string? stationId = null)
        {
            Expression<Func<Cache.Entities.Sensor, bool>>? whereExpression = null;

            if (ids is { Length: > 0 })
            {
                whereExpression = (sensor) => ids.Contains(sensor.SensorId);
            }

            if (!string.IsNullOrEmpty(stationId))
            {
                Expression<Func<Cache.Entities.Sensor, bool>> stationExpression = (sensor) => sensor.StationId == stationId;

                whereExpression = whereExpression != null ? whereExpression.And(stationExpression) : stationExpression;
            }

            if (whereExpression != null)
            {
                return await _sensorCacheRepository.GetAllAsync(whereExpression);
            }

            return await _sensorCacheRepository.GetAllAsync();
        }

        private Cache.Entities.Sensor MapSensorValueToSensor(SensorValue sensorValue)
        {
            return new Cache.Entities.Sensor
            {
                Id = $"{sensorValue.StationId}{sensorValue.Id}",
                SensorId = sensorValue.Id.ToString(),
                StationId = sensorValue.StationId.ToString(),
                Name = sensorValue.Name,
                MeasuredTime = sensorValue.MeasuredTime,
                TimeWindowStart = sensorValue.TimeWindowStart,
                TimeWindowEnd = sensorValue.TimeWindowEnd,
                Value = sensorValue.Value,
                Unit = sensorValue.Unit
            };
        }

        private TimeSpan GetExpireSpan(SensorValue sensorValue)
        {
            TimeSpan sensorTimeSpan = TimeSpan.Zero;

            if (_fastExpireSensorIds.Contains(sensorValue.Id))
            {
                sensorTimeSpan = TimeSpan.FromMinutes(5);
            }
            else if (_slowExpireSensorIds.Contains(sensorValue.Id))
            {
                sensorTimeSpan = TimeSpan.FromMinutes(60);
            }

            TimeSpan expireSpan = DateTime.UtcNow.Add(sensorTimeSpan) - DateTime.UtcNow;

            return expireSpan;
        }
    }
}