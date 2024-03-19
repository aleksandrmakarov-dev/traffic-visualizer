using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Data.Repositories.Cache;
using TukkoTrafficVisualizer.Data.Repositories.Database;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
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

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IMailingService, MockMailingService>();
            services.AddSingleton<IPasswordsService, BcryptPasswordsService>();
            services.AddSingleton<ITokensService, TokensService>();
            services.AddSingleton<IJwtService, JwtService>();

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

            return services;
        }

        public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JsonWebTokenOptions>(builder.Configuration.GetSection(JsonWebTokenOptions.Name));
            builder.Services.Configure<MailingOptions>(builder.Configuration.GetSection(MailingOptions.Name));
            builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.Name));

            return builder;

        }
    }
}
