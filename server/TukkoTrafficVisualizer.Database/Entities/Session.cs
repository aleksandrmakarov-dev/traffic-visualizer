using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Database.Entities
{
    public class Session:Entity
    {
        [MaxLength(256)]
        public required string RefreshToken{ get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
