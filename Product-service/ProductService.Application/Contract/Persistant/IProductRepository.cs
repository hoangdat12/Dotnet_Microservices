using ProductService.Domain.Entity;


namespace ProductService.Application.Contract.Persistant
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> IsExistInShop(Guid shopId, string productName);
        Task<List<Product>> GetProducts(List<Guid> productIds);
        Task<List<Product>> GetProductsOfShop(List<Guid> productIds, Guid shopId);
    }
}
