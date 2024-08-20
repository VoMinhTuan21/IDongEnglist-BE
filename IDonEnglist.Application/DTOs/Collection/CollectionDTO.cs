using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media;

namespace IDonEnglist.Application.DTOs.Collection
{
    public class CollectionDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int ThumbnailId { get; set; }
        public MediaDTO Thumbnail { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
