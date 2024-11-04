using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Models.Pagination;
using System.Linq.Expressions;

namespace IDonEnglist.Application.Persistence.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, bool withDeleted = false, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<PaginatedList<T>> GetPaginatedListAsync(
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> sortBy = null,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 10,
            bool withDeleted = false,
            Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<List<T>> GetAllListAsync(
            Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> sortBy = null,
            bool ascending = true, bool withDeleted = false,
            Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T> AddAsync(T entity, CurrentUser? currentUser = null);
        Task<T> UpdateAsync(T entity, CurrentUser currentUser);
        Task<T?> DeleteAsync(int id, CurrentUser currentUser);
        Task<bool> ExistsAsync(int id);
        Task AddRangeAsync(IEnumerable<T> entities, CurrentUser currentUser);
        Task UpdateRangeAsync(IEnumerable<T> entities, CurrentUser currentUser);
    }
}
