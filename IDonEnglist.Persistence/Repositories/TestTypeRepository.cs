using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;

namespace IDonEnglist.Persistence.Repositories
{
    public class TestTypeRepository : GenericRepository<TestType>, ITestTypeRepository
    {
        public TestTypeRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
        }
    }
}
