

using ProductService.Application.Dto;
using ProductService.Domain.Entity;

namespace ProductService.Application.Contract.Persistant
{
    public interface IDiscountRepository: IGenericRepository<Discount>
    {
        public Task<Discount> GetByDiscountCodeAsync(string discountCode);
        public Task<List<Discount>> GetListDiscountAsync(Pagination pagination);
        public Task<List<Discount>> GetShopDiscountAsync(Guid ShopId, Pagination pagination);
    }
}