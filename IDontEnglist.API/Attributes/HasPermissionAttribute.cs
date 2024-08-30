using Microsoft.AspNetCore.Authorization;

namespace IDonEnglist.API.Attributes
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(policy: permission)
        {

        }
    }
}
