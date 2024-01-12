using Microsoft.EntityFrameworkCore;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext.Repository
{
    public class UserRepository(
        ApplicationDbContext context
    ) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
