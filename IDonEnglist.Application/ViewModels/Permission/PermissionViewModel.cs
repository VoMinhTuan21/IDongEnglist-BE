using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.Permission
{
    public class PermissionViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        public PermissionViewModel? Parent { get; set; }
        public ICollection<PermissionViewModel> Children { get; set; }
    }
}
