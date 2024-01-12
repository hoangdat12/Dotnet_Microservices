using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Persistence.DatabaseContext.Repository
{
   public class ClothingRepository(ApplicationDbContext context) : GenericRepository<Clothing>(context), IClothingRepository
    {
        public async Task DeleteByProductId(Guid productId)
        {
            await _context.Clothings
                .Where(c => c.ProductId == productId)
                .ExecuteDeleteAsync();
        }
    }
}