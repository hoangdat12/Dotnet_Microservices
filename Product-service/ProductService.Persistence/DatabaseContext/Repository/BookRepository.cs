
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Persistence.DatabaseContext.Repository
{
    public class BookRepository(ApplicationDbContext context) : GenericRepository<Book>(context), IBookRepository
    {
        public async Task DeleteByProductId(Guid productId)
        {
            await _context.Books
                .Where(b => b.ProductId == productId)
                .ExecuteDeleteAsync();
        }
    }
}