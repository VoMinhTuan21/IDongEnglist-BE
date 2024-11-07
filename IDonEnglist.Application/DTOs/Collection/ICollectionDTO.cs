using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Collection
{
    public interface ICollectionDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public FileDTO Thumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
