using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class TestPartRepository : GenericRepository<TestPart>, ITestPartRepository
    {
        public TestPartRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
