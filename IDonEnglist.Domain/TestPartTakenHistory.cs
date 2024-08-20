using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class TestPartTakenHistory : BaseDomainEntity
    {
        public int TestPartId { get; set; }
        public TestPart TestPart { get; set; }
        public int TestTakenHistoryId { get; set; }
        public TestTakenHistory TestTakenHistory { get; set; }
        public TestTakenStatus Status { get; set; }
        public float Score { get; set; }
        public int Duration { get; set; }

        public ICollection<TestSectionTakenHistory> Sections { get; set; }
    }
}
