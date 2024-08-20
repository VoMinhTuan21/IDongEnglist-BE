using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Passage : BaseDomainEntity
    {
        public string Content { get; set; }

        public ICollection<TestSection> TestSections { get; set; }
    }
}
