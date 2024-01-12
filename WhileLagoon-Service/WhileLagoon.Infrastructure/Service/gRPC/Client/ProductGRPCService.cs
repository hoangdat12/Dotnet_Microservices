using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ProductService;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Dto;

namespace WhileLagoon.Infrastructure.Service
{
    public class ProductGRPCService: IProductGRPCService
    {
        private readonly IConfiguration _configuration;
        private readonly GrpcServer _gprcServer;

        public ProductGRPCService(IConfiguration configuration) {
            _configuration = configuration;

             GrpcServer grpcServer = new();
            _configuration.GetSection(AppSetting.GrpcServer).Bind(grpcServer);
            _gprcServer = grpcServer;
        }

        public async Task<GetProductRes> GetProduct(string ProductId)
        {
            using var channel = GrpcChannel.ForAddress(_gprcServer.ProductServer);
            var _client = new ProductGRPC.ProductGRPCClient(channel);

            GetProductReq request = new()
            {
                ProductId = ProductId
            };
            return await _client.GetProductAsync(request);
        } 
    }
}