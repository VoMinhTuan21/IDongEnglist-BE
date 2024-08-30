using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IDonEnglist.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
    {
        private readonly IDonEnglistDBContext _dbContext;

        public GenericRepository(IDonEnglistDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);

            return entity;
        }

        public async Task<T?> DeleteAsync(int id, int currentUserId)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                entity.DeletedBy = currentUserId;
                entity.DeletedDate = DateTime.Now;
                _dbContext.Entry(entity).State = EntityState.Modified;
                return entity;
            }

            return null;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            return entity != null;
        }

        public async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(
            Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> sortBy = null,
            bool ascending = true, int pageNumber = 1, int pageSize = 10, bool withDeleted = false,
            Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!withDeleted)
            {
                query = query.Where(e => e.DeletedDate == null && e.DeletedBy == null);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (sortBy != null)
            {
                query = ascending ? query.OrderBy(sortBy) : query.OrderByDescending(sortBy);
            }

            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PaginatedList<T>(items, pageNumber, pageSize, totalRecords);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, bool withDeleted = false,
            Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!withDeleted)
            {
                query = query.Where(e => e.DeletedDate == null && e.DeletedBy == null);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
        }

        public async Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> sortBy = null, bool ascending = true,
            bool withDeleted = false, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (!withDeleted)
            {
                query = query.Where(e => e.DeletedDate == null && e.DeletedBy == null);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (sortBy != null)
            {
                query = ascending ? query.OrderBy(sortBy) : query.OrderByDescending(sortBy);
            }

            return await query.ToListAsync();
        }
    }
}
