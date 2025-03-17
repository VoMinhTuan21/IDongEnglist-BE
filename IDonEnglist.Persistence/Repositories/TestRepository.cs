using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
