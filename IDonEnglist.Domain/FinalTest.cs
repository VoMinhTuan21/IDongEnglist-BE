using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class FinalTest : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }

        public ICollection<Test> Tests { get; set; }
    }
}
