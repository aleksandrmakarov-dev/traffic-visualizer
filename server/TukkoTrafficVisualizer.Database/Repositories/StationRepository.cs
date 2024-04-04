using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;

namespace TukkoTrafficVisualizer.Database.Repositories
{
    public class StationRepository:GenericRepository<Station>,IStationRepository
    {
        public StationRepository(IMongoClient client, IClientSessionHandle session) : base(client, session)
        {
        }

        public async Task UpdateByIdAsync(Station station, ReplaceOptions options)
        {

            await Collection.ReplaceOneAsync(s=>s.StationId == station.StationId, station,options);
        }

        public override async Task<Station?> GetByIdAsync(string id)
        {
            var aggr = Collection.Aggregate()
                .Match(e=>e.StationId == id)
                .Lookup<Station, Station>(
                    foreignCollectionName:nameof(Sensor),
                    localField:nameof(Station.StationId),
                    foreignField:nameof(Sensor.StationId),
                    @as:nameof(Station.Sensors)
                );
            return await aggr.FirstOrDefaultAsync();
        }
    }
}
