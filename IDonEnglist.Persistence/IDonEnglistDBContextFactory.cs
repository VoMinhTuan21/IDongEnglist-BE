using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IDonEnglist.Persistence
{
    public class IDonEnglistDBContextFactory : IDesignTimeDbContextFactory<IDonEnglistDBContext>
    {
        public IDonEnglistDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<IDonEnglistDBContext>();
            var connectionString = configuration.GetConnectionString("IDonEnglistConnectionString");

            builder.UseSqlServer(connectionString);

            return new IDonEnglistDBContext(builder.Options);
        }
    }
}
