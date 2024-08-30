using Microsoft.AspNetCore.Authorization;

namespace IDonEnglist.API.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
