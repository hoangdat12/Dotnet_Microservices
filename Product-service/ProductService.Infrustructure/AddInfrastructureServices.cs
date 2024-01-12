using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Constant;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Logging;
using ProductService.Application.Dto.AppSetting;
using ProductService.Application.Event;
using ProductService.Infrustructure.Logging;
using ProductService.Infrustructure.Service;
using StackExchange.Redis;
using RabbitMQ.Client;
using ProductService.Application.Contract.RabbitMq;
using ProductService.Infrustructure.Service.RabbitMq;

namespace ProductService.Infrustructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastrutureServices(
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));   

            RedisConfiguration redisConfiguration = new();
            configuration.GetSection(AppSetting.RedisConfiguration).Bind(redisConfiguration);

            // Add the configured RedisConfiguration to the services if needed
            services.AddSingleton(redisConfiguration);
            services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString)
            );
            services.AddStackExchangeRedisCache(option => 
                option.Configuration = redisConfiguration.ConnectionString
            );
            services.AddSingleton<IRedisService, RedisService>();

            services.AddScoped<IInventoryGRPCClient, InventoryGRPCClient>();
            services.AddScoped<IShopGRPCClient, ShopGRPCClient>();
            services.AddScoped<IUserGRPCClient, UserGRPCClient>();

            services.AddScoped<IProductFactory, ProductFactory>();
            services.AddScoped<BookService>();
            services.AddScoped<ClothingService>();
            services.AddScoped<FurnitureService>();
            services.AddScoped<ElectronicService>();

            services.AddScoped<IRabbitMqClient, RabbitMqClient>();

            return services;
        }
    }

}
