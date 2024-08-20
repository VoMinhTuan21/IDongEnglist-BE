using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Collection : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int ThumbnailId { get; set; }
        public Media Thumbnail { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<FinalTest> FinalTests { get; set; }
    }
}
