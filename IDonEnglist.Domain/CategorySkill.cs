using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class CategorySkill : BaseDomainEntity
    {
        public Skill Skill { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TestTypeId { get; set; }
        public TestType TestType { get; set; }
    }
}
