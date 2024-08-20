using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Role : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
