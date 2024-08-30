using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<HashSet<string>> GetPermissionsAsync(int userId)
        {
            var userWithDetails = await _unitOfWork.UserRepository
                .GetByIdAsync(userId, query => query.Include(u => u.Role)
                                                    .ThenInclude(u => u.RolePermissions)
                                                    .ThenInclude(u => u.Permission));
            if (userWithDetails == null)
            {
                return [];
            }

            return userWithDetails.Role.RolePermissions.Select(u => u.Permission.Name).ToHashSet();
        }
    }
}
