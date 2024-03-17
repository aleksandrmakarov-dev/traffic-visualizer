using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Services
{
    public interface ILocationService
    {
        public Task<IEnumerable<LocationResponse>> FindByQueryAsync(string query);
    }
}
