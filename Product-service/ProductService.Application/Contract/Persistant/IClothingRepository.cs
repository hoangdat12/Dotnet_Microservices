
using ProductService.Domain.Entity;

namespace ProductService.Application.Contract.Persistant
{
    public interface IClothingRepository: IGenericRepository<Clothing>
    {
        public Task DeleteByProductId(Guid productId);
    }
}