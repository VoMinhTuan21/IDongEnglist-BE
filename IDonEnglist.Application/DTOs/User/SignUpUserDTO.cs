namespace IDonEnglist.Application.DTOs.User
{
    public class SignUpUserDTO : IUserDTO
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
    }
}
