using IDonEnglist.Application.Constants;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace IDonEnglist.API.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            string userIdClaimm = context.User.Claims.FirstOrDefault(
                x => x.Type == CustomClaimTypes.Id)?.Value ?? "0";

            if (!int.TryParse(userIdClaimm, out _))
            {
                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            HashSet<string> permissions = await userService.GetPermissionsAsync(Int32.Parse(userIdClaimm));

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
            else
            {
                throw new ForbiddenException("You don't have permission");
            }
        }
    }
}
