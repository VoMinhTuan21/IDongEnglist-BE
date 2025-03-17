using IDonEnglist.Application.Persistence.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace IDonEnglist.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDonEnglistDBContext _dbContext;
        private IDbContextTransaction _transaction;

        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IPermissionRepository _permissionRepository;
        private IRolePermissionRepository _rolePermissionRepository;
        private ICategorySkillRepository _categorySkillRepository;
        private ICollectionRepository _collectionRepository;
        private IMediaRepository _mediaRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private ITestPartRepository _testPartRepository;
        private ITestTypeRepository _testTypeRepository;
        private IFinalTestRepository _finalTestRepository;
        private ITestRepository _testRepository;

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

        public ICollectionRepository CollectionRepository
        {
            get
            {
                _collectionRepository ??= new CollectionRepository(_dbContext);
                return _collectionRepository;
            }
        }

        public IMediaRepository MediaRepository
        {
            get
            {
                _mediaRepository ??= new MediaRepository(_dbContext);
                return _mediaRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                _refreshTokenRepository ??= new RefreshTokenRepository(_dbContext);
                return _refreshTokenRepository;
            }
        }

        public ITestTypeRepository TestTypeRepository
        {
            get
            {
                _testTypeRepository ??= new TestTypeRepository(_dbContext);
                return _testTypeRepository;
            }
        }

        public ITestPartRepository TestPartRepository
        {
            get
            {
                _testPartRepository ??= new TestPartRepository(_dbContext);
                return _testPartRepository;
            }
        }

        public IFinalTestRepository FinalTestRepository
        {
            get
            {
                _finalTestRepository ??= new FinalTestRepository(_dbContext);
                return _finalTestRepository;
            }
        }

        public ITestRepository TestRepository
        {
            get
            {
                _testRepository ??= new TestRepository(_dbContext);
                return _testRepository;
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

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _dbContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
