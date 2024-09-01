using System.ComponentModel;

namespace IDonEnglist.Application.DTOs.User
{
    public class LoginUserDTO
    {
        [DefaultValue("abcd@gmail.com")]
        public string Email { get; set; }
        [DefaultValue("@Nguyenvana22")]
        public string Password { get; set; }
    }
}
