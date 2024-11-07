using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Application.ViewModels.Common;
using IDonEnglist.Application.ViewModels.TestPart;

namespace IDonEnglist.Application.ViewModels.TestType
{
    public class TestTypeDetailViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Questions { get; set; }
        public int Duration { get; set; }
        public List<TestPartViewModel> Parts { get; set; }
        public CategorySkillWithParentViewModel CategorySkill { get; set; }
    }
}
