using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext.Repository
{
    public class ShopRepository(ApplicationDbContext context) :
        GenericRepository<Shop>(context), IShopRepository
    {
        public IDbContextTransaction BeginTransaction()
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction;
        }

        public async Task<Shop> GetByShopName(string shopName)
        {
            return await _context.Shops
                .Where(s => s.ShopName == shopName)
                .FirstOrDefaultAsync();
        }
    }
}
