using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
