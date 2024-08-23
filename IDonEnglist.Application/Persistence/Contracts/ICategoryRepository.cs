using IDonEnglist.Domain;

namespace IDonEnglist.Application.Persistence.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetAllAsync(bool isHierarchy = false);
    }
}
