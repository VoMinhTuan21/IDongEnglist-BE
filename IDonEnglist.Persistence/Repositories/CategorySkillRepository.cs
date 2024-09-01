using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class CategorySkillRepository : GenericRepository<CategorySkill>, ICategorySkillRepository
    {
        public CategorySkillRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
