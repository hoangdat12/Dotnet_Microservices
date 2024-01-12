using OrderService.Domain.Entity;

namespace OrderService.Application.Contract.Persistence
{
    public interface IOrderCheckoutRepository: IGenericRepository<OrderCheckout>
    {
        public Task<OrderCheckout> GetByOrderIdAsync(Guid orderId);
    }
}
