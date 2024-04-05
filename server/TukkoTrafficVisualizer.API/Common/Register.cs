using TukkoTrafficVisualizer.Cache.Interfaces;
using TukkoTrafficVisualizer.Cache.Repositories;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Database.Interfaces;
using TukkoTrafficVisualizer.Database.Repositories;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API.Common
{
    public static class Register
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRoadworkCacheService, RoadworkCacheService>();
            services.AddScoped<ISensorCacheService, SensorCacheService>();
            services.AddScoped<IStationCacheService, StationCacheService>();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IStationService, StationService>();
            services.AddScoped<ISensorService, SensorService>();

            services.AddScoped<ILocationService, HttpLocationService>();
            services.AddScoped<IMailingService, MockMailingService>();
            services.AddSingleton<IPasswordsService, BcryptPasswordsService>();
            services.AddSingleton<ITokensService, TokensService>();
            services.AddSingleton<IJwtService, JwtService>();

            services.AddScoped<IStationHttpService, StationHttpService>();
            services.AddScoped<ISensorHttpService, SensorHttpService>();
            services.AddScoped<IRoadworkHttpService, RoadworkHttpService>();


            return services;
        }

        public static IServiceCollection AddCacheRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoadworkCacheRepository, RoadworkCacheRepository>();
            services.AddScoped<ISensorCacheRepository, SensorsCacheRepository>();
            services.AddScoped<IStationCacheRepository, StationCacheRepository>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository,UsersRepository>();
            services.AddScoped<ISessionsRepository,SessionsRepository>();
            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();

            return services;
        }

        public static WebApplicationBuilder AddAppOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JsonWebTokenOptions>(builder.Configuration.GetSection(JsonWebTokenOptions.Name));
            builder.Services.Configure<MailingOptions>(builder.Configuration.GetSection(MailingOptions.Name));
            builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.Name));

            return builder;

        }
    }
}
