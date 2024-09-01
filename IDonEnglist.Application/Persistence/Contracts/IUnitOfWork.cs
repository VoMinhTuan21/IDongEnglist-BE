namespace IDonEnglist.Application.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IRolePermissionRepository RolePermissionRepository { get; }
        ICategorySkillRepository CategorySkillRepository { get; }
        Task Save();
    }
}
