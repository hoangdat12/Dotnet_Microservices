using OrderService.Domain.Entity;

namespace OrderService.Application.Contract.Persistence
{
    public interface IOrderProductRepository: IGenericRepository<OrderProduct>
    {
        public Task<OrderProduct> GetProductByProductIdAsync(Guid productId);
    }
}
