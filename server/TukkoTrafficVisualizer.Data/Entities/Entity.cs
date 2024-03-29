﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Redis.OM.Modeling;

namespace TukkoTrafficVisualizer.Data.Entities
{
    public abstract class Entity
    {
        [RedisIdField]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Indexed(Sortable = true)]
        public string Id { get; set; }
    }
}
