using Microsoft.EntityFrameworkCore.Storage;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Repository
{
    public interface IShopRepository: IGenericRepository<Shop>
    {
        Task<Shop> GetByShopName(string shopName);
        IDbContextTransaction BeginTransaction();
    }
}
