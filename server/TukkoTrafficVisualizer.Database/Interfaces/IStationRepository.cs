﻿using MongoDB.Driver;
using TukkoTrafficVisualizer.Database.Entities;

namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface IStationRepository:IGenericRepository<Station>
    {
        Task UpdateByIdAsync(Station station, ReplaceOptions options);
    }
}
