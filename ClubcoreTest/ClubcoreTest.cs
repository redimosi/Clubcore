using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Clubcore.Infrastructure;
using Clubcore.Entities;
using System.Linq;

namespace ClubcoreTest
{
    public class ClubcoreDbContextTests
    {
        private DbContextOptions<ClubcoreDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ClubcoreDbContext>();
            builder.UseInMemoryDatabase("ClubcoreTest")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Test]
        public void CanAddClub()
        {
            var options = CreateNewContextOptions();

            // Run the test against one instance of the context
            using (var context = new ClubcoreDbContext(options))
            {
                var club = new Club { Name = "Test Club" };
                context.Clubs.Add(club);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ClubcoreDbContext(options))
            {
                Assert.AreEqual(1, context.Clubs.Count());
                Assert.AreEqual("Test Club", context.Clubs.Single().Name);
            }
        }
    }
}