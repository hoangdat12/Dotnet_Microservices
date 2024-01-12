
using ShopGRPCService;

namespace OrderService.Application.Contract.Infrastructure
{
    public interface IShopGRPCClient
    {
        public Task<GetShopRes> GetShopAsync(string ShopId); 
    }
}