using Microsoft.EntityFrameworkCore;
using OrderService.Application.Contract.Persistence;
using OrderService.Application.Dto;
using OrderService.Domain.Entity;

namespace OrderService.Persistence.DatabaseContext.Repository
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
    {
        public async Task<List<Order>> GetByUserIdAsync(Guid userId, Pagination pagination)
        {
            int page = pagination.Page;
            int limit = pagination.Limit;
            int offset = (page - 1) * limit;

            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Order> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderProducts)
                .Include (o => o.OrderCheckout)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetByShopIdAsync(Guid shopId, Pagination pagination)
        {
            int page = pagination.Page;
            int limit = pagination.Limit;
            int offset = (page - 1) * limit;

            return await _context.Orders
                .Where(o => o.ShopId == shopId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
    }
}
