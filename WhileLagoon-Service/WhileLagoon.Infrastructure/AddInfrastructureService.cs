using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Client;
using WhileLagoon.Application.Contract.Logging;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Dto;
using WhileLagoon.Infrastructure.Logging;
using WhileLagoon.Infrastructure.Service;
using WhileLagoon.Infrastructure.Service.rabbitMq;

namespace WhileLagoon.Infrastructure
{
    public static class AddInfrastructureService
    {
        public static IServiceCollection ConfigureInfrastrutureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IS3Service, S3Service>();
            services.AddScoped<IProductGRPCService, ProductGRPCService>();

            RedisConfiguration redisConfiguration = new();
            configuration.GetSection(AppSetting.RedisConfiguration).Bind(redisConfiguration);

            // Add the configured RedisConfiguration to the services if needed
            services.AddSingleton(redisConfiguration);
            services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString)
            );
            services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            services.AddSingleton<IRedisService, RedisService>();

            services.AddScoped<IRabbitMqClient, RabbitMqClient>();

            return services;
        }
    }
}
