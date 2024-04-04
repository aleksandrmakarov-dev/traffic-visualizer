using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;

namespace TukkoTrafficVisualizer.Database.Repositories
{
    public class SensorRepository:GenericRepository<Sensor>,ISensorRepository
    {
        public SensorRepository(IMongoClient client, IClientSessionHandle session) : base(client, session)
        {
        }

        public async Task UpdateBySensorIdAsync(Sensor sensor, ReplaceOptions options)
        {
            await Collection.ReplaceOneAsync(s=>s.SensorId == sensor.SensorId, sensor, options);
        }
    }
}
