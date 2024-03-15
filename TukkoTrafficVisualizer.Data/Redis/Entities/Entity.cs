using Redis.OM.Modeling;

namespace TukkoTrafficVisualizer.Data.Redis.Entities
{
    public abstract class Entity
    {
        [Indexed]
        public string Id { get; set; }
    }
}
