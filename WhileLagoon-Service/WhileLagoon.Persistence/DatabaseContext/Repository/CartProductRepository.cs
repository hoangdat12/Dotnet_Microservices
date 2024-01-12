
using Microsoft.EntityFrameworkCore;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext.Repository
{
    public class CartProductRepository(ApplicationDbContext context) : GenericRepository<CartProduct>(context), ICartProductRepository
    {
        public async Task<CartProduct> GetProductInCartAsync(Guid ProductId, Guid CartId)
        {
            return await _context.CartProducts
                .Where(cp => (cp.ProductId == ProductId) && (cp.CartId == CartId))
                .FirstOrDefaultAsync();
        }

        public async Task<List<CartProduct>> GetProductsAsync(Guid CartId, int Page = 1, int Limit = 20)
        {
            int offset = (Page - 1) * Limit;
            return await _context.CartProducts
                .Where(cp => cp.CartId == CartId)
                .OrderBy(cp => cp.CreatedAt)
                .Skip(offset)
                .Take(Limit)
                .ToListAsync();
        }
    }
}