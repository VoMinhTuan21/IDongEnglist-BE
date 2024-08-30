using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Permission
{
    public class PermissionDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
