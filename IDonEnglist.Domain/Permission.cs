using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Permission : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
