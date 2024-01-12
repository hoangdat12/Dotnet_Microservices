using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Contract.Infrastructure.gRPC;
using OrderService.Infrastructure.Common;
using ProductService;

namespace OrderService.Infrastructure.Service
{
    public class ProductGrpcClient : BaseClient, IProductGrpcClient
    {
        private readonly ProductGRPC.ProductGRPCClient _client;
        public ProductGrpcClient(IConfiguration configuration): base(configuration)
        {
            var channel = GrpcChannel.ForAddress(_grpcServer.ProductServer);
            _client = new ProductGRPC.ProductGRPCClient(channel);
        }
        public async Task<GetPriceRes> GetPriceAsync(List<string> productIds, string shopId)
        {
            
            ProductIds productIdsReq = new();
            productIdsReq.Ids.AddRange(productIds);

            GetProductsReq getPriceReq = new()
            {
                ProductIds = productIdsReq,
                ShopId = shopId
            };
           
            var reply = await _client.GetPricesAsync(getPriceReq);
            return reply;
        }

        public async Task<Products> GetProductsAsync(List<string> productIds)
        {
            ProductIds productIdsReq = new();
            productIdsReq.Ids.AddRange(productIds);
          
            var reply = await _client.GetProductByIdsAsync(productIdsReq);
            return reply;
        }
    }
}