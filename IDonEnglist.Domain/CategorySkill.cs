using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class CategorySkill : BaseDomainEntity
    {
        public Skill Skill { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<TestType> TestTypes { get; set; }
    }
}
