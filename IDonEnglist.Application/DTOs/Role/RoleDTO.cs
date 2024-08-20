using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Role
{
    public class RoleDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
