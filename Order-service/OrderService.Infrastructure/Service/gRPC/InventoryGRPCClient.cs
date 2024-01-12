
using Grpc.Net.Client;
using InventoryGRPCService;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Infrastructure.Common;

namespace OrderService.Infrustructure.Service
{
    public class InventoryGRPCClient: BaseClient, IInventoryGRPCClient
    {
        private readonly InventoryGRPC.InventoryGRPCClient _client;
        public InventoryGRPCClient(IConfiguration configuration): base(configuration)
        {
            var channel = GrpcChannel.ForAddress(_grpcServer.ProductServer);
            _client = new InventoryGRPC.InventoryGRPCClient(channel);
        }

        public async Task<GetInventoryRes> GetInventory(string ProductId)
        {
            GetInventoryReq request = new()
            {
                ProductId = ProductId
            };
            return await _client.GetInventoryAsync(request);
        }
    }
}