using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.Category
{
    public interface ICategoryDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
