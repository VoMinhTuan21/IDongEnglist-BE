using IDonEnglist.Application.Persistence.Contracts;

namespace IDonEnglist.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDonEnglistDBContext _dbContext;

        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

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
