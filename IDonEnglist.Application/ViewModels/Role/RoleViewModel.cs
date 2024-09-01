using IDonEnglist.Application.ViewModels.Common;
using IDonEnglist.Application.ViewModels.Permission;

namespace IDonEnglist.Application.ViewModels.Role
{
    public class RoleViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        private ICollection<PermissionViewModel>? _permissions;
        public ICollection<PermissionViewModel>? Permissions { get => BuildPermissionTree(); set { _permissions = value; } }

        private ICollection<PermissionViewModel> BuildPermissionTree()
        {
            if (_permissions == null)
            {
                return new List<PermissionViewModel>();
            }

            var parentPermissions = _permissions
                .Where(x => x.Parent != null)
                .GroupBy(c => c.Parent.Id)
                .Select(g => new PermissionViewModel
                {
                    Id = g.Key,
                    Name = g.First().Parent.Name,
                    Code = g.First().Parent.Code,
                    Children = g.Select(c => new PermissionViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Code = c.Code,
                    }).ToList()
                }).ToList();

            return parentPermissions;
        }
    }
}
