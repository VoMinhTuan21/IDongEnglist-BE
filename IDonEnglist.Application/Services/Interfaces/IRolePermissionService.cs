using IDonEnglist.Application.Models.Identity;

namespace IDonEnglist.Application.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task UpdateRolePermissionsAsync(int roleId, List<int> permissionIds, CurrentUser currentUser);
        Task DeleteRolePermissionAsync(int roleId, List<int> permissionIds, CurrentUser currentUser);
        Task DeleteRolePermissionAsync(int roleId, CurrentUser currentUser);
    }
}
