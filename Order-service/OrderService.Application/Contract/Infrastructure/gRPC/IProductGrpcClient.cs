

using ProductService;

namespace OrderService.Application.Contract.Infrastructure.gRPC
{
    public interface IProductGrpcClient
    {
        public Task<GetPriceRes> GetPriceAsync(List<string> productIds, string shopId);
        public Task<Products> GetProductsAsync(List<string> productIds);
    }
}