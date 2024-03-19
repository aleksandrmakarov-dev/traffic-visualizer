using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Infrastructure.Models.Contracts;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IRoadworkService
    {
        Task<RoadworkContract> FetchRoadworkAsync();
        Task SaveRoadworksAsync(RoadworkContract roadworkContract);
        Task<IEnumerable<Roadwork>> GetAsync(int primaryPointRoadNumber, int primaryPointRoadSection,
            int secondaryPointRoadNumber, int secondaryPointRoadSection, string severity);
    }
}
