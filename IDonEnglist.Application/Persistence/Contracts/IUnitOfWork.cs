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
        ICollectionRepository CollectionRepository { get; }
        IMediaRepository MediaRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        ITestTypeRepository TestTypeRepository { get; }
        ITestPartRepository TestPartRepository { get; }
        IFinalTestRepository FinalTestRepository { get; }
        Task Save();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task BeginTransactionAsync();
    }
}
