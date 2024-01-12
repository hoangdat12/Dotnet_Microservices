

using InventoryGRPCService;

namespace ProductService.Application.Contract.Infrastructure
{
    public interface IInventoryGRPCClient
    {
        public Task<GetInventoryRes> GetInventory(string ProductId);
    }
}