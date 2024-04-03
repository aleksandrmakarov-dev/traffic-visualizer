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

        public async Task<Station> UpdateByStationIdAsync(Station station, ReplaceOptions options)
        {
            var builder = new UpdateDefinitionBuilder<Station>()
                .Set(s => s.Name, station.Name);

            await Collection.ReplaceOneAsync(s=>s.StationId == station.StationId, station,options);

            return station;
        }
    }
}
