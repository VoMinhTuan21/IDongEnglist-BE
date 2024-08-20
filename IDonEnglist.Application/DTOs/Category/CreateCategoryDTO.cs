namespace IDonEnglist.Application.DTOs.Category
{
    public class CreateCategoryDTO : ICategoryDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public int? ParentId { get; set; }
    }
}
