using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Category : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public ICollection<Category>? Children { get; set; }
        public ICollection<CategorySkill>? Skills { get; set; }
        public ICollection<Collection>? Collections { get; set; }
    }
}
