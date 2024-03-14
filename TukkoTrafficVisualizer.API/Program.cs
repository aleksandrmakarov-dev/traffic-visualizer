using System.Reflection;
using System.Text.Json;
using StackExchange.Redis;
using TukkoTrafficVisualizer.API.BackgroundServices;
using TukkoTrafficVisualizer.API.Common;
using TukkoTrafficVisualizer.Data.Redis.Entities;
using TukkoTrafficVisualizer.Infrastructure.Services;

namespace TukkoTrafficVisualizer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Logger
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add HttpClient
            builder.Services.AddHttpClient<ILocationService,NominatimLocationService>(client =>
            {
                client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            });

            // Add Redis

            string? redisConnectionString = builder.Configuration.GetConnectionString("Redis");

            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new ArgumentException("No redis connection string found");
            }

            // Add repositories
            builder.Services.AddRepositories();
            // Add Services
            builder.Services.AddServices();

            //Add Automapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(),
                Assembly.Load("TukkoTrafficVisualizer.Infrastructure"));

            //Add Background services

            builder.Services.AddHostedService<UpdateCacheBackgroundService>();
            builder.Services.AddHostedService<IndexCreationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();

            //app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            Roadwork rw = new Roadwork
            {
                Direction = "test",
                Id = "123",
                PrimaryPointRoadNumber = 1,
                SecondaryPointRoadNumber = 2,
                PrimaryPointRoadSection = 2,
                SecondaryPointRoadSection = 10,
                Severity = "HIGH",
                EndTime = DateTime.Now,
                StartTime = DateTime.Now.AddDays(-1),
                Restrictions = new List<Restriction>
                {
                    new Restriction
                    {
                        Name = "test",
                        Quantity = 1,
                        Type = "Test",
                        Unit = "km/h"
                    }
                }
            };

            var json = JsonSerializer.Serialize(rw);

            redis.GetDatabase().SetAdd("test123", json);

            app.Run();
        }
    }
}
