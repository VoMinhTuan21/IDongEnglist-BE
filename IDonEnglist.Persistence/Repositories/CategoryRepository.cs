using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly IDonEnglistDBContext _dbContext;

        public CategoryRepository(IDonEnglistDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Category>> GetAll(bool isHierarchy = false)
        {
            if (isHierarchy)
            {
                var categories = await _dbContext.Categories.Where(c => c.ParentId == null)
                    .Include(c => c.Children).ToListAsync();

                return categories.AsReadOnly();
            }
            else
            {
                var categories = await _dbContext.Categories.ToListAsync();
                return categories.AsReadOnly();
            }
        }
    }
}
