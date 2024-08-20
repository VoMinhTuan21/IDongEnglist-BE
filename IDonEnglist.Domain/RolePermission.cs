using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class RolePermission : BaseDomainEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
