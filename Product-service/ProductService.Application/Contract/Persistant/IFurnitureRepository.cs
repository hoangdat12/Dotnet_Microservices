
using ProductService.Domain.Entity;

namespace ProductService.Application.Contract.Persistant
{
    public interface IFurnitureRepository: IGenericRepository<Furniture>
    {
        public Task DeleteByProductId(Guid productId);
    }
}