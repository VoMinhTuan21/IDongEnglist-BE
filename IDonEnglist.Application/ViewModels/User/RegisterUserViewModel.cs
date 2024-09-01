using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.User
{
    public class RegisterUserViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
