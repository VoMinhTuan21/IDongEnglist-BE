using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class FinalTestRepository : GenericRepository<FinalTest>, IFinalTestRepository
    {
        public FinalTestRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
