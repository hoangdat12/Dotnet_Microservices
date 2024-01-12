

using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Repository
{
    public interface ICartProductRepository: IGenericRepository<CartProduct>
    {
        public Task<CartProduct> GetProductInCartAsync(Guid ProductId, Guid CartId);
        public Task<List<CartProduct>> GetProductsAsync(Guid CartId, int Page = 1, int Limit = 20);
    }
}