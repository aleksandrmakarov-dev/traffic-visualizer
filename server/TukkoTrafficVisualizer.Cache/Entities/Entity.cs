using Redis.OM.Modeling;

namespace TukkoTrafficVisualizer.Cache.Entities
{
    public abstract class Entity
    {
        [RedisIdField]
        [Indexed(Sortable = true)]
        public string Id { get; set; }
    }
}
