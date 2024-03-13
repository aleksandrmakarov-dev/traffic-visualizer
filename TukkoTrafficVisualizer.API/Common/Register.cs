﻿using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.Common
{
    public static class Register
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILocationService, NominatimLocationService>();
            services.AddScoped<IRoadworkService, RoadworkService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
