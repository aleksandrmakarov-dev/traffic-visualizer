using Redis.OM.Modeling;

namespace TukkoTrafficVisualizer.Data.Entities
{
    public abstract class Entity
    {
        [RedisIdField]
        public string Id { get; set; }
    }
}
