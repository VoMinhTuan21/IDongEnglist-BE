using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
