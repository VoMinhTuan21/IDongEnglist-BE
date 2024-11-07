using IDonEnglist.Application.ViewModels.Category;
using IDonEnglist.Application.ViewModels.Common;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.ViewModels.CategorySkill
{
    public class CategorySkillWithParentViewModel : BaseViewModel
    {
        public Skill Skill { get; set; }
        public CategoryMiniViewModel Category { get; set; }
    }
}
