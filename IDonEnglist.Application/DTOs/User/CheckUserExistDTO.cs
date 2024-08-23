namespace IDonEnglist.Application.DTOs.User
{
    public class CheckUserExistDTO : IUserDTO
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
