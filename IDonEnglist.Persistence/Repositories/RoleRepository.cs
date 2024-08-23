using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
