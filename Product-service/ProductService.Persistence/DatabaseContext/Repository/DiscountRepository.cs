

using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Dto;
using ProductService.Domain.Entity;

namespace ProductService.Persistence.DatabaseContext.Repository
{
    public class DiscountRepository(ApplicationDbContext context)
        : GenericRepository<Discount>(context), IDiscountRepository
    {
        public async Task<Discount> GetByDiscountCodeAsync(string discountCode)
        {
            return await _context.Discounts
                .Where(d => d.DiscountCode == discountCode)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Discount>> GetListDiscountAsync(Pagination pagination)
        {
            int page = pagination.Page;
            int limit = pagination.Limit;
            int offset = (page - 1) * limit;

            return await _context.Discounts
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Discount>> GetShopDiscountAsync(Guid ShopId, Pagination pagination)
        {
            int page = pagination.Page;
            int limit = pagination.Limit;
            int offset = (page - 1) * limit;

            return await _context.Discounts
                .Where(d => d.DiscountShopId == ShopId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
    }
}