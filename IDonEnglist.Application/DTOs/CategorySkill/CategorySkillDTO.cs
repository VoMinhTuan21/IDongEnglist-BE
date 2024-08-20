using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.TestType;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.CategorySkill
{
    public class CategorySkillDTO : BaseDTO
    {
        public Skill Skill { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public int TestTypeId { get; set; }
        public TestTypeDTO TestType { get; set; }
    }
}
