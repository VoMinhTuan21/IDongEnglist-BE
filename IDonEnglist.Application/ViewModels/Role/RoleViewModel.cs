using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.ViewModels.Role
{
    public class RoleViewModel : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
