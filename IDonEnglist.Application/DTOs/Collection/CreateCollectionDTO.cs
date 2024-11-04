using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Collection
{
    public class CreateCollectionDTO : ICollectionDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public FileDTO Thumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
