namespace IDonEnglist.Application.ViewModels.Permission
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        public PermissionViewModel? Parent { get; set; }
        public ICollection<PermissionViewModel> Children { get; set; }
    }
}
