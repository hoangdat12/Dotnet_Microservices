using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Persistence.DatabaseContext.Repository
{
    public class ProductRepository
        (ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetProducts(List<Guid> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsOfShop(List<Guid> productIds, Guid shopId)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.Id) && p.ProductShop == shopId)
                .ToListAsync();
        }

        public async Task<Product> IsExistInShop(Guid shopId, string productName)
        {
            return await _context.Products.Where(
                p => p.ProductName == productName && p.ProductShop == shopId
            ).FirstOrDefaultAsync();
        }
    }
}
 