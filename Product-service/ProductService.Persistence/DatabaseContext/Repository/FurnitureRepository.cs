using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Persistence.DatabaseContext.Repository
{
    public class FurnitureRepository(ApplicationDbContext context) : GenericRepository<Furniture>(context), IFurnitureRepository
    {
        public async Task DeleteByProductId(Guid productId)
        {
              await _context.Electronics
                .Where(c => c.ProductId == productId)
                .ExecuteDeleteAsync();
        }
    }
}