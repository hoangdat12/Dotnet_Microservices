using Microsoft.EntityFrameworkCore;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext.Repository
{
    public class InventoryRepository(ApplicationDbContext context) : GenericRepository<Inventory>(context), IInventoryRepository
    {
        public async Task<Inventory> GetByProductIdAsync(Guid ProductId)
        {
            return await _context.Inventories
                .Where(i => i.ProductId == ProductId)
                .FirstOrDefaultAsync();
        }
    }
}