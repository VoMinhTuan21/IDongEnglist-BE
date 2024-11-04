using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class RefreshToken : BaseDomainEntity
    {
        public string Token { get; set; }
        public string TokenRefresh { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
