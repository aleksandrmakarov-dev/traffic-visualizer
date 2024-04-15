using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Redis.OM;
using Server.API.Middlewares;
using StackExchange.Redis;
using TukkoTrafficVisualizer.API.BackgroundServices;
using TukkoTrafficVisualizer.API.Common;
using TukkoTrafficVisualizer.API.Hubs;
using TukkoTrafficVisualizer.API.Middlewares;
using TukkoTrafficVisualizer.Core.Options;

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
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            // Add Logger
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add HttpClient
            builder.Services.AddHttpClient(Core.Constants.Constants.NominatimHttpClientName, client =>
            {
                client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            });

            builder.Services.AddHttpClient(Core.Constants.Constants.DigiTrafficHttpClientName, client =>
            {
                client.BaseAddress = new Uri("https://tie.digitraffic.fi/api/");
            }).ConfigurePrimaryHttpMessageHandler(handler=>new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            builder.Services.RemoveAll<IHttpMessageHandlerBuilderFilter>();


            // Add Redis
            builder.Services.AddSingleton(sp =>
            {
                RedisOptions options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;

                return new RedisConnectionProvider(new RedisConnectionConfiguration
                {
                    Host = builder.Environment.IsDevelopment() ? options.Development : options.Production,
                    Port = options.Port,
                    Password = options.Password
                });
            });

            // Add Mongodb
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                MongoDbOptions options = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;

                var mongoUrl = new MongoUrlBuilder
                {
                    Server = new MongoServerAddress(builder.Environment.IsDevelopment() ? options.Development : options.Production),
                    Username = options.Username,
                    Password = options.Password,
                    AuthenticationSource = "admin"
                }.ToMongoUrl();

                return new MongoClient(MongoClientSettings.FromUrl(mongoUrl));
            });

            builder.Services.AddScoped(c => c.GetRequiredService<IMongoClient>().StartSession());

            // Add cache repositories
            builder.Services.AddCacheRepositories();

            // Add repositories
            builder.Services.AddRepositories();

            // Add Options
            builder.AddAppOptions();

            // Add Services

            // Remove builder.Environment.IsDevelopment() to don't use mock mailing and feedback services in debug
            builder.Services.AddServices(builder.Environment.IsDevelopment());

            //Add Automapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(),
                Assembly.Load("TukkoTrafficVisualizer.Infrastructure"));

            //Add Background services

            builder.Services.AddHostedService<IndexCreationBackgroundService>();
            builder.Services.AddHostedService<UpdateBackgroundService>();

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapHub<NotificationHub>("api/notifications");

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseResponseCompression();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // user extractor middleware
            app.UseMiddleware<UserExtractorMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
