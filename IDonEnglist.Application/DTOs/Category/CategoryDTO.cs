using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Category
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public List<CategoryDTO>? Children { get; set; }
    }
}
