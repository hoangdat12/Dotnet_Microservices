using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Infrustructure.Common;
using ShopGRPCService;

namespace ProductService.Infrustructure.Service
{
    public class ShopGRPCClient(IConfiguration configuration) : BaseClient(configuration), IShopGRPCClient
    {
        public async Task<GetShopRes> GetShopAsync(string ShopId)
        {
            using var channel = GrpcChannel.ForAddress(_grpcServer.BaseServer);
            var client = new ShopGRPC.ShopGRPCClient(channel);

            GetShopReq request = new()
            {
                ShopId = ShopId
            };

            return await client.GetShopAsync(request);
        }
    }
}