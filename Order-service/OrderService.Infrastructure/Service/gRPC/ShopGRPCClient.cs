using System.Text.Json;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Infrastructure.Common;
using ShopGRPCService;

namespace OrderService.Infrustructure.Service
{
    public class ShopGRPCClient : BaseClient, IShopGRPCClient
    {
        private readonly ShopGRPC.ShopGRPCClient _client;
        public ShopGRPCClient(IConfiguration configuration): base(configuration)
        {
            var channel = GrpcChannel.ForAddress(_grpcServer.BaseServer);
            _client = new ShopGRPC.ShopGRPCClient(channel);
        }
        public async Task<GetShopRes> GetShopAsync(string ShopId)
        {

            GetShopReq request = new()
            {
                ShopId = ShopId
            };

            GetShopRes foundShop = await _client.GetShopAsync(request);
            return foundShop;
        }
    }
}