
using Microsoft.EntityFrameworkCore;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext.Repository
{
    public class CartRepository(ApplicationDbContext context) : GenericRepository<Cart>(context), ICartRepository
    {
        public async Task<Cart> GetByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}