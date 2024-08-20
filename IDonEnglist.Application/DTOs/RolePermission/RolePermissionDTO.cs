using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Permission;
using IDonEnglist.Application.DTOs.Role;

namespace IDonEnglist.Application.DTOs.RolePermission
{
    public class RolePermissionDTO : BaseDTO
    {
        public int RoleId { get; set; }
        public RoleDTO Role { get; set; }
        public int PermissionId { get; set; }
        public PermissionDTO Permission { get; set; }
    }
}
