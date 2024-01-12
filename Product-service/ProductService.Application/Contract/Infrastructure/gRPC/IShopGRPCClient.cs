
using ShopGRPCService;

namespace ProductService.Application.Contract.Infrastructure
{
    public interface IShopGRPCClient
    {
        public Task<GetShopRes> GetShopAsync(string ShopId); 
    }
}