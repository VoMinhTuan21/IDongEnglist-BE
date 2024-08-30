using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.ViewModels.User;

namespace IDonEnglist.Identity.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterUserViewModel> SignUp(SignUpUserDTO signUpData);
        Task<LoginUserViewModel> Login(LoginUserDTO loginData);
        Task<RefreshTokenViewModel> Refresh(RefreshTokenDTO refreshData);
    }
}
