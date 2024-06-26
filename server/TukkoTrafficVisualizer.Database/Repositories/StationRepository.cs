﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;

namespace TukkoTrafficVisualizer.Database.Repositories
{
    public class StationRepository:GenericRepository<Station>,IStationRepository
    {
        public StationRepository(IMongoClient client, IClientSessionHandle session) : base(client, session)
        {
        }

        public async Task ReplaceByIdAsync(Station station, ReplaceOptions options)
        {
            await Collection.ReplaceOneAsync(s=>s.StationId == station.StationId, station,options);
        }

        public async Task<Station?> GetByStationIdAsync(string stationId)
        {
            return await Collection.AsQueryable().FirstOrDefaultAsync(e=>e.StationId == stationId);
        }

        public async Task<Station?> GetByStationIdWithSensorsAsync(string id,DateTime start,DateTime end)
        {
            Station? station = await Collection.AsQueryable().FirstOrDefaultAsync(s => s.StationId == id);

            if (station != null)
            {
                station.Sensors = await Database
                    .GetCollection<Sensor>(nameof(Sensor))
                    .AsQueryable()
                    .Where(s=>s.StationId == id)
                    .Where(s=>s.MeasuredTime>=start && s.MeasuredTime <= end)
                    .ToListAsync();
            }

            return station;
        }
    }
}
