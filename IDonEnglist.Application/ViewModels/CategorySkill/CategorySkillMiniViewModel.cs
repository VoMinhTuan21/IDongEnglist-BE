using IDonEnglist.Application.ViewModels.Common;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.ViewModels.CategorySkill
{
    public class CategorySkillMiniViewModel : BaseViewModel
    {
        public Skill Skill { get; set; }
        public bool IsConfigured { get; set; } = false;
    }
}
