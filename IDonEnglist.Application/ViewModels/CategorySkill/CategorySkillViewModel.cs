using IDonEnglist.Application.ViewModels.Common;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.ViewModels.CategorySkill
{
    public class CategorySkillViewModel : BaseViewModel
    {
        public int CategoryId { get; set; }
        public Skill Skill { get; set; }
    }
}
