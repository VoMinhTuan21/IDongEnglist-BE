using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Test : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? AudioId { get; set; }
        public Media? Audio { get; set; }
        public int TestTaken { get; set; }
        public int FinalTestId { get; set; }
        public FinalTest FinalTest { get; set; }
        public int TestTypeId { get; set; }
        public TestType TestType { get; set; }

        public ICollection<TestSection> Sections { get; set; }
        public ICollection<TestTakenHistory> TestTakenHistories { get; set; }
    }
}
