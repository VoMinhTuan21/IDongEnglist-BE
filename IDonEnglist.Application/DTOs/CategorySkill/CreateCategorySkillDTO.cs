using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.CategorySkill
{
    public class CreateCategorySkillDTO
    {
        public int CategoryId { get; set; }
        public Skill Skill { get; set; }
    }
}
