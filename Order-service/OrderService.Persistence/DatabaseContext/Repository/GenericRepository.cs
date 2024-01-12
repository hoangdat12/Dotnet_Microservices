using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Application.Contract.Persistence;

namespace OrderService.Persistence.DatabaseContext.Repository
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        protected IDbContextTransaction _transaction;
        
        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void StartTransaction() 
        {
            _transaction ??= _context.Database.BeginTransaction();
        }

        public void EndTransaction()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw; // Re-throw the exception for the calling code to handle
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
        private void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
    }
}
