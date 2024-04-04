using TukkoTrafficVisualizer.Database.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class SensorService:ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly int[] _sensorIds = [5054, 5055, 5056, 5057, 5067, 5071];

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task SaveAsync(SensorContract sensorContract)
        {
            foreach (Models.Contracts.Station station in sensorContract.Stations)
            {
                // ignore sensors
                IEnumerable<SensorValue> sensorValues =
                    station.SensorValues.Where(sv => _sensorIds.Contains(sv.Id));

                foreach (SensorValue sensorValue in sensorValues)
                {

                    Database.Entities.Sensor sensor = MapSensorValueToSensor(sensorValue);

                    await _sensorRepository.CreateAsync(sensor);
                }
            }
        }

        private Database.Entities.Sensor MapSensorValueToSensor(SensorValue sensorValue)
        {
            return new Database.Entities.Sensor
            {
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
    }
}
