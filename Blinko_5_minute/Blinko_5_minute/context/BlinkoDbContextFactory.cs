using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Blinko_5_minute.context
{
    public class BlinkoDbContextFactory : IDesignTimeDbContextFactory<BlinkoDBContext>
    {
        public BlinkoDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BlinkoDBContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new BlinkoDBContext(optionsBuilder.Options);
        }
    }
}
