namespace WhileLagoon.Application.Contract.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        // Task<List<T>> GetAsync(Pagination pagination);
        Task<T> GetByIdAsync(Guid id);
    }
}
