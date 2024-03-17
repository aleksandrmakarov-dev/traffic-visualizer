using TukkoTrafficVisualizer.Data.Repositories;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.Common
{
    public static class Register
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILocationService, NominatimLocationService>();
            services.AddScoped<IRoadworkService, RoadworkService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IStationService, StationService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoadworkCacheRepository, RoadworkCacheRepository>();
            services.AddScoped<ISensorCacheRepository, SensorsCacheRepository>();
            services.AddScoped<IStationCacheRepository, StationCacheRepository>();

            return services;
        }
    }
}
