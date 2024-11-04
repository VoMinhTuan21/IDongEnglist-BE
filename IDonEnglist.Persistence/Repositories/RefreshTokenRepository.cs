using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
