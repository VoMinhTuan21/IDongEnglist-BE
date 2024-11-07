using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.Category
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public ICollection<CategoryViewModel> Children { get; set; }
        public List<CategorySkillMiniViewModel>? Skills { get; set; }
    }
}
