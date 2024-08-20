using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class TestTakenHistory : BaseDomainEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public float Score { get; set; }
        public int Duration { get; set; }
        public TestTakenStatus Status { get; set; }

        public ICollection<TestPartTakenHistory> Parts { get; set; }
    }
}
