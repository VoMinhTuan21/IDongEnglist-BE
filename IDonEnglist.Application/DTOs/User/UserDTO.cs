using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Role;

namespace IDonEnglist.Application.DTOs.User
{
    public class UserDTO : BaseDTO
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public RoleDTO Role { get; set; }
    }
}
