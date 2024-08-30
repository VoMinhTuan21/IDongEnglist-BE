using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Permission : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }

        public Permission? Parent { get; set; }
        public ICollection<Permission>? Children { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
