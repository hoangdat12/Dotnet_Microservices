
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Repository
{
    public interface IInventoryRepository: IGenericRepository<Inventory>
    {
        public Task<Inventory> GetByProductIdAsync(Guid ProductId);
    }
}