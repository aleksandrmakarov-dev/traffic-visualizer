using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using MongoDB.Driver;
using Redis.OM;
using TukkoTrafficVisualizer.API.BackgroundServices;
using TukkoTrafficVisualizer.API.Common;
using TukkoTrafficVisualizer.API.Middlewares;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Logger
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add HttpClient
            builder.Services.AddHttpClient<ILocationService,NominatimLocationService>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                }
                    );



            if (builder.Environment.IsDevelopment())
            {
                // Add Redis
                builder.Services.AddSingleton(
                    new RedisConnectionProvider("redis://localhost:6379"));
                // Add MongoDB

                builder.Services.AddSingleton<IMongoClient>(c => new MongoClient("mongodb://admin:admin@localhost:27017"));
            }
            else
            {
                // Add Redis
                builder.Services.AddSingleton(
                    new RedisConnectionProvider("redis://tukko-redis-v2:6379"));

                // Add MongoDB
                builder.Services.AddSingleton<IMongoClient>(c => new MongoClient("mongodb://admin:admin@tukko-archive-v2:27017"));
            }

            builder.Services.AddScoped(c => c.GetRequiredService<IMongoClient>().StartSession());

            // Add cache repositories
            builder.Services.AddCacheRepositories();

            // Add repositories
            builder.Services.AddRepositories();

            // Add Options
            builder.Services.AddOptions();

            // Add Services
            builder.Services.AddServices();

            //Add Automapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(),
                Assembly.Load("TukkoTrafficVisualizer.Infrastructure"));

            //Add Background services

            builder.Services.AddHostedService<UpdateCacheBackgroundService>();
            builder.Services.AddHostedService<IndexCreationBackgroundService>();

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseResponseCompression();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
