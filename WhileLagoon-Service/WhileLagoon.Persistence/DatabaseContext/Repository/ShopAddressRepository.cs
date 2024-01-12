using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext.Repository
{
    public class ShopAddressRepository(ApplicationDbContext context)
        : GenericRepository<ShopAddress>(context), IShopAddressRepository
    {
        public Task<ShopAddress> GetByShopId(Guid ShopId)
        {
            throw new NotImplementedException();
        }
    }
}
