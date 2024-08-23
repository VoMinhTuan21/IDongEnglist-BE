using System.Linq.Expressions;

namespace IDonEnglist.Application.Persistence.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> filter);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
