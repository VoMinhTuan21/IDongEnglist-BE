using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Role
{
    public class UpdateRoleDTO : BaseDTO, IRoleDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public List<int>? PermissionIds { get; set; }
    }
}
