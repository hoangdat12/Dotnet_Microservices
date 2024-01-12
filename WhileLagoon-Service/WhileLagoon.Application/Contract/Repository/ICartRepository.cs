using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Repository
{
    public interface ICartRepository: IGenericRepository<Cart>
    {
        public Task<Cart> GetByUserIdAsync(Guid userId);
    }
}