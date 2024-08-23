namespace IDonEnglist.Application.DTOs.Role
{
    public class CreateRoleDTO : IRoleDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
