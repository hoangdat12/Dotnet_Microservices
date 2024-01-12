
namespace ProductService.Application.Contract.Persistant
{
    public interface IProductBaseRepository
    {
        public Task DeleteByProductId(Guid ProductId);
    }
}