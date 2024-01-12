using OrderService.Application.Dto;
using OrderService.Domain.Entity;


namespace OrderService.Application.Contract.Persistence
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        public Task<List<Order>> GetByUserIdAsync(Guid userId, Pagination pagination);
        public Task<List<Order>> GetByShopIdAsync(Guid shopId, Pagination pagination);
        public Task<Order> GetByOrderIdAsync(Guid orderId);
    }
}
