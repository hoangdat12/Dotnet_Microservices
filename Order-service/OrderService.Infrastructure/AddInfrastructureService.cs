using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Application.Contract.Infrastructure.gRPC;
using OrderService.Infrastructure.Service;
using OrderService.Infrustructure.Service;

namespace OrderService.Infrastructure
{
    public static class AddInfrastructureService
    {
        public static IServiceCollection ConfigureInfrastrutureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IProductGrpcClient, ProductGrpcClient>();
            services.AddScoped<IInventoryGRPCClient, InventoryGRPCClient>();
            services.AddScoped<IShopGRPCClient, ShopGRPCClient>();
            services.AddScoped<IUserGRPCClient, UserGRPCClient>();
            
            return services;
        }
    }
}
