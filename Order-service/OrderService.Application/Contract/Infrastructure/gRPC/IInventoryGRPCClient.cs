

using InventoryGRPCService;

namespace OrderService.Application.Contract.Infrastructure
{
    public interface IInventoryGRPCClient
    {
        public Task<GetInventoryRes> GetInventory(string ProductId);
    }
}