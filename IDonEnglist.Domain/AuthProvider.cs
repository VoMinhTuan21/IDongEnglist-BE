using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class AuthProvider : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
        public string? RedirectUri { get; set; }

        public ICollection<UserSocialAccount> UserSocialAccounts { get; set; }
    }
}
