using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.CategorySkill
{
    public class UpdateCategorySkillDTO
    {
        public int CategoryId { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
