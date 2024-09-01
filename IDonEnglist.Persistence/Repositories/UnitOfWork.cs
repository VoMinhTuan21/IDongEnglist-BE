using IDonEnglist.Application.Persistence.Contracts;

namespace IDonEnglist.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDonEnglistDBContext _dbContext;

        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IPermissionRepository _permissionRepository;
        private IRolePermissionRepository _rolePermissionRepository;
        private ICategorySkillRepository _categorySkillRepository;

        public UnitOfWork(IDonEnglistDBContext context)
        {
            _dbContext = context;
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                _categoryRepository ??= new CategoryRepository(_dbContext);
                return _categoryRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_dbContext);
                return _userRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                _roleRepository ??= new RoleRepository(_dbContext);
                return _roleRepository;
            }
        }

        public IPermissionRepository PermissionRepository
        {
            get
            {
                _permissionRepository ??= new PermissionRepository(_dbContext);
                return _permissionRepository;
            }
        }

        public IRolePermissionRepository RolePermissionRepository
        {
            get
            {
                _rolePermissionRepository ??= new RolePermissionRepository(_dbContext);
                return _rolePermissionRepository;
            }
        }

        public ICategorySkillRepository CategorySkillRepository
        {
            get
            {
                _categorySkillRepository ??= new CategorySkillRepository(_dbContext);
                return _categorySkillRepository;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
