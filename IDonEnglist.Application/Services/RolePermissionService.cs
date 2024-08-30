using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Services.Interfaces;
using IDonEnglist.Domain;

namespace IDonEnglist.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolePermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteRolePermissionAsync(int roleId, List<int> permissionIds, int currentUserId)
        {
            var deleteRolePermissions = await _unitOfWork.RolePermissionRepository
                .GetAllListAsync(rp => rp.RoleId == roleId && permissionIds.Contains(rp.Id));

            foreach (var rolePermission in deleteRolePermissions)
            {
                await _unitOfWork.RolePermissionRepository.DeleteAsync(rolePermission.Id, currentUserId);
            }

            await _unitOfWork.Save();
        }

        public async Task DeleteRolePermissionAsync(int roleId, int currentUserId)
        {
            var deletedRolePermissions = await _unitOfWork.RolePermissionRepository.GetAllListAsync(rp => rp.RoleId == roleId);

            foreach (var rolePermission in deletedRolePermissions)
            {
                await _unitOfWork.RolePermissionRepository.DeleteAsync(rolePermission.Id, currentUserId);
            }

            await _unitOfWork.Save();
        }

        public async Task UpdateRolePermissionsAsync(int roleId, List<int> permissionIds, int currentUserId)
        {
            if (permissionIds == null || permissionIds.Count == 0) { return; }

            var rolePermission = await _unitOfWork.RolePermissionRepository.GetAllListAsync(rp => rp.RoleId == roleId, null, true, true);

            var deletedRolePermission = rolePermission.Where(
                rp => !permissionIds.Contains(rp.PermissionId) &&
                rp.DeletedBy == null && rp.DeletedDate == null).ToList();

            var addedRolePermissions = new List<RolePermission>();

            foreach (var permissionId in permissionIds)
            {
                var existingRolePermission = rolePermission.FirstOrDefault(rp => rp.PermissionId == permissionId);
                if (existingRolePermission != null)
                {
                    if (existingRolePermission.DeletedBy != null && existingRolePermission.DeletedDate != null)
                    {
                        existingRolePermission.DeletedDate = null;
                        existingRolePermission.DeletedBy = null;
                        await _unitOfWork.RolePermissionRepository.UpdateAsync(existingRolePermission);
                    }
                }
                else
                {
                    addedRolePermissions.Add(new RolePermission { PermissionId = permissionId, RoleId = roleId, CreatedBy = currentUserId });
                }
            }

            if (addedRolePermissions.Any())
            {
                await _unitOfWork.RolePermissionRepository.AddRangeAsync(addedRolePermissions);
            }

            await DeleteRolePermissionAsync(roleId, deletedRolePermission.Select(a => a.Id).ToList(), currentUserId);

            await _unitOfWork.Save();
        }
    }
}
