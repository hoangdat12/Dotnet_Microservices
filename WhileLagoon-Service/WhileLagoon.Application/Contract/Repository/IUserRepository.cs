using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Contract.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByEmailAsync(string email);
    }
}
