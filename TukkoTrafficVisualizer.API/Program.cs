using System.Reflection;
using Server.API.Middlewares;
using StackExchange.Redis;
using TukkoTrafficVisualizer.API.BackgroundServices;
using TukkoTrafficVisualizer.API.Common;
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

            var httpClient2 = new HttpClient();

            // Add HttpClient
            builder.Services.AddHttpClient(nameof(NominatimLocationService),(services,client)=>
            {
                client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            });

            // Add Redis

            string redisConnectionString = builder.Configuration.GetConnectionString("Redis");

            builder.Services.AddSingleton<ConnectionMultiplexer>(
                ConnectionMultiplexer.Connect("localhost"));

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

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();


            app.Run();
        }
    }
}
