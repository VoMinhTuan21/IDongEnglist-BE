using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Permission
{
    public class UpdatePermissionDTO : BaseDTO, IPermissionDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
