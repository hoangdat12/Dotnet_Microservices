using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Repository
{
    public interface IShopAddressRepository : IGenericRepository<ShopAddress>
    {
        Task<ShopAddress> GetByShopId(Guid ShopId);
    }
}
