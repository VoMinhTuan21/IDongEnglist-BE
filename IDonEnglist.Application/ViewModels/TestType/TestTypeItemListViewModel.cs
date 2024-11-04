using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.TestType
{
    public class TestTypeItemListViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Questions { get; set; }
        public int Duration { get; set; }
        public int Parts { get; set; }
        public CategorySkillWithParentViewModel CategorySkill { get; set; }
    }
}
