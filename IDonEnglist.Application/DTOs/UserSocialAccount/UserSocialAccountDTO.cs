using IDonEnglist.Application.DTOs.AuthProvider;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.User;

namespace IDonEnglist.Application.DTOs.UserSocialAccount
{
    public class UserSocialAccountDTO : BaseDTO
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int AuthProviderId { get; set; }
        public AuthProviderDTO AuthProvider { get; set; }
        public string ProviderUserId { get; set; }
        public string AccessToken { get; set; }
    }
}
