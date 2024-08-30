namespace IDonEnglist.Application.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task UpdateRolePermissionsAsync(int roleId, List<int> permissionIds, int currentUserId);
        Task DeleteRolePermissionAsync(int roleId, List<int> permissionIds, int currentUserId);
        Task DeleteRolePermissionAsync(int roleId, int currentUserId);
    }
}
