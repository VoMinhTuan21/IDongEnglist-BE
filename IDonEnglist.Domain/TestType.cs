using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class TestType : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfParts { get; set; }
        public int Duration { get; set; }
        public int CategorySkillId { get; set; }

        public ICollection<TestPart> TestParts { get; set; }
        public ICollection<Test> Tests { get; set; }
        public CategorySkill CategorySkill { get; set; }
    }
}
