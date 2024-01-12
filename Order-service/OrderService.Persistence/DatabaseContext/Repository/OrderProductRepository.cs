using Microsoft.EntityFrameworkCore;
using OrderService.Application.Contract.Persistence;
using OrderService.Domain.Entity;

namespace OrderService.Persistence.DatabaseContext.Repository
{
    public class OrderProductRepository(ApplicationDbContext context)
        : GenericRepository<OrderProduct>(context), IOrderProductRepository
    {
        public async Task<OrderProduct> GetProductByProductIdAsync(Guid productId)
        {
            return await _context.OrderProducts
                .Where(o => o.ProductId == productId)
                .FirstOrDefaultAsync();
        }
    }
}
