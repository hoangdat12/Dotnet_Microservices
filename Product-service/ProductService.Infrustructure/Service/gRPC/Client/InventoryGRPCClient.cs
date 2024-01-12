
using Grpc.Net.Client;
using InventoryGRPCService;
using Microsoft.Extensions.Configuration;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Infrustructure.Common;

namespace ProductService.Infrustructure.Service
{
    public class InventoryGRPCClient(IConfiguration configuration): BaseClient(configuration), IInventoryGRPCClient
    {
        public async Task<GetInventoryRes> GetInventory(string ProductId)
        {
            using var channel = GrpcChannel.ForAddress(_grpcServer.BaseServer);
            var _client = new InventoryGRPC.InventoryGRPCClient(channel);

            GetInventoryReq request = new()
            {
                ProductId = ProductId
            };
            return await _client.GetInventoryAsync(request);
        }
    }
}