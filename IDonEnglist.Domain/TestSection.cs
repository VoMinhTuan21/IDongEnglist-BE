using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class TestSection : BaseDomainEntity
    {
        public int TestPartId { get; set; }
        public TestPart TestPart { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public string? Instruction { get; set; }
        public int? PassageId { get; set; }
        public Passage? Passage { get; set; }

        public ICollection<QuestionGroup> QuestionGroups { get; set; }
        public ICollection<TestSectionTakenHistory> TestSectionTakenHistories { get; set; }
    }
}
