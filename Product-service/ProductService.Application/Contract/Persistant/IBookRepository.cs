using ProductService.Domain.Entity;

namespace ProductService.Application.Contract.Persistant
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        public Task DeleteByProductId(Guid productId);
    }
}
