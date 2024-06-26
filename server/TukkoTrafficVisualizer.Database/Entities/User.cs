﻿using System.ComponentModel.DataAnnotations;

namespace TukkoTrafficVisualizer.Database.Entities
{
    public class User : Entity
    {
        public required string Email { get; set; }
        [MaxLength(256)]
        public required string PasswordHash { get; set; }
        [MaxLength(256)]
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiresAt { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        [MaxLength(64)]
        public required string Role { get; set; }
        public IEnumerable<string> FavoriteStations { get; set; } = new List<string>();
    }
}
