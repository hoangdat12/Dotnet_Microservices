

using ProductService;

namespace WhileLagoon.Application.Contract.Service
{
    public interface IProductGRPCService
    {
        public Task<GetProductRes> GetProduct(string ProductId);
    }
}