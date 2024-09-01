using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Category
{
    public class UpdateCategoryDTO : BaseDTO, ICategoryDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? ParentId { get; set; }
    }
}
