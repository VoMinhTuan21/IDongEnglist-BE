using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.User
{
    public class LoginUserViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
