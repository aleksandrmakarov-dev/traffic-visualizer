using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public interface IRoadworkService
    {
        Task<RoadworkContract?> FetchLatestRoadworkAsync();
        Task SaveRoadworkAsync(RoadworkContract roadworkContract);

        Task<IEnumerable<Data.Redis.Entities.Roadwork>> FilterAsync(int primaryPointRoadNumber,
            int primaryPointRoadSection, int secondaryPointRoadNumber, int secondaryPointRoadSection,
            DateTime startTimeOnAfter, DateTime startTimeOnBefore, string severity);
    }
}
