﻿using System.Net.Http.Json;
using System.Net;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;
using TukkoTrafficVisualizer.Data.Repositories.Cache;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public class SensorService : ISensorService
    {
        private readonly HttpClient _httpClient;
        private readonly ISensorCacheRepository _sensorCacheRepository;
        private readonly int[] _fastExpireSensorIds = [5058, 5061, 5116, 5119, 5122, 5125, 5158, 5161, 5164, 5168];
        private readonly int[] _slowExpireSensorIds = [5054, 5055, 5056, 5057];
        private readonly int[] _ignoreSensorIds = [5016, 5019, 5022, 5025, 5064, 5067, 5068, 5071, 5152,];

        public SensorService(HttpClient httpClient, ISensorCacheRepository sensorCacheRepository)
        {
            // creating new http client because injected client is not working
            _httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            _sensorCacheRepository = sensorCacheRepository;
        }

        public async Task<SensorContract> FetchSensorsAsync()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://tie.digitraffic.fi/api/tms/v1/stations/data");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new BadRequestException($"Failed to fetch sensors: {responseMessage.ReasonPhrase}");
            }

            SensorContract? sensorContract = await responseMessage.Content.ReadFromJsonAsync<SensorContract>();

            if (sensorContract == null)
            {
                throw new InternalBufferOverflowException("Failed to fetch sensors");
            }

            return sensorContract;
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
                    Sensor sensor = MapSensorValueToSensor(sensorValue);

                    TimeSpan expireSpan = GetExpireSpan(sensorValue);

                    if (expireSpan > TimeSpan.Zero)
                    {
                        await _sensorCacheRepository.SetAsync(sensor, expireSpan);
                    }
                }
            }
        }

        public async Task<IEnumerable<Sensor>> GetAsync(
        )
        {
            // IEnumerable<Data.Redis.Entities.Roadwork> roadworkList = await _roadworkCacheRepository.FilterAsync(
            //    primaryPointRoadNumber, primaryPointRoadSection, secondaryPointRoadNumber, secondaryPointRoadSection,
            //    startTimeOnAfter, startTimeOnBefore, severity);


            return await _sensorCacheRepository.GetAsync();
        }

        private Sensor MapSensorValueToSensor(SensorValue sensorValue)
        {
            return new Sensor
            {
                Id = $"{sensorValue.StationId}_{sensorValue.Id}",
                SensorId = sensorValue.Id,
                StationId = sensorValue.StationId,
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
            else if(_slowExpireSensorIds.Contains(sensorValue.Id))
            {
                sensorTimeSpan = TimeSpan.FromMinutes(60);
            }

            TimeSpan expireSpan = DateTime.UtcNow.Add(sensorTimeSpan) - DateTime.UtcNow;

            return expireSpan;
        }
    }
}
