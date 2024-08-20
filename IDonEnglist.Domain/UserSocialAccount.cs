using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class UserSocialAccount : BaseDomainEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int AuthProviderId { get; set; }
        public AuthProvider AuthProvider { get; set; }
        public string ProviderUserId { get; set; }
        public string AccessToken { get; set; }
    }
}
