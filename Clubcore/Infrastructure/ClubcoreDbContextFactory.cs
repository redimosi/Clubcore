using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Clubcore.Infrastructure
{
    public class ClubcoreDbContextFactory : IDesignTimeDbContextFactory<ClubcoreDbContext>
    {
        public ClubcoreDbContext CreateDbContext(string[] args)
        {
            // Adjust the path to locate the appsettings.json in the ClubcoreApi project directory
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ClubcoreDbContext>();
            var connectionString = configuration.GetConnectionString("ClubcoreDb");
            optionsBuilder.UseNpgsql(connectionString);

            return new ClubcoreDbContext(optionsBuilder.Options);
        }
    }
}

