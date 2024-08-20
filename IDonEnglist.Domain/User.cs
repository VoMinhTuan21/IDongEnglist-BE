using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class User : BaseDomainEntity
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<UserSocialAccount> UserSocialAccounts { get; set; }
        public ICollection<TestTakenHistory> TestTakenHistories { get; set; }
    }
}
