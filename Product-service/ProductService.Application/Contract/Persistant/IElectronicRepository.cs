
using ProductService.Domain.Entity;

namespace ProductService.Application.Contract.Persistant
{
    public interface IElectronicRepository: IGenericRepository<Electronic>
    {
        public Task DeleteByProductId(Guid productId);
    }
}