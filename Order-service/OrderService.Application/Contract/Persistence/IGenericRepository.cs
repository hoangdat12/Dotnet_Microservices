namespace OrderService.Application.Contract.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        // Task<List<T>> GetAsync(Pagination pagination);
        Task<T> GetByIdAsync(Guid id);
        void StartTransaction();
        void EndTransaction();
    }
}
