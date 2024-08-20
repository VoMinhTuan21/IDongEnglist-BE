using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class TestSectionTakenHistory : BaseDomainEntity
    {
        public int TestPartTakenHistoryId { get; set; }
        public TestPartTakenHistory TestPartTakenHistory { get; set; }
        public int TestSectionId { get; set; }
        public TestSection TestSection { get; set; }
        public TestTakenStatus Status { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
