using Microsoft.EntityFrameworkCore;
using OrderService.Application.Contract.Persistence;
using OrderService.Domain.Entity;

namespace OrderService.Persistence.DatabaseContext.Repository
{
    public class OrderCheckoutRepository(ApplicationDbContext context)
        : GenericRepository<OrderCheckout>(context), IOrderCheckoutRepository
    {
        public async Task<OrderCheckout> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderCheckouts
                .Where(oc => oc.OrderId == orderId)
                .FirstOrDefaultAsync();
        }
    }
}
