using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class TestPart : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public int NumberOfQuestions { get; set; }
        public int Order { get; set; }
        public int TestTypeId { get; set; }
        public TestType TestType { get; set; }

        public ICollection<TestPartTakenHistory> TestPartTakenHistories { get; set; }
        public ICollection<TestSection> Sections { get; set; }
    }
}
