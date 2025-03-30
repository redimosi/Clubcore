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

        private static void SeedData(DbContextOptions<ClubcoreDbContext> options)
        {
            using (var context = new ClubcoreDbContext(options))
            {
                var person = new Person { Name = new PersonName { FirstName = "Test", LastName = "Person" } };
                var group = new Group { Name = "Test Group 1" };
                var role = new Role { Name = "Test Role" };
                context.Clubs.Add(new Club { Name = "Test Club" });
                context.SaveChanges();
            }
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
                Assert.That(context.Clubs.Count(), Is.EqualTo(1));
                Assert.That(context.Clubs.Single().Name, Is.EqualTo("Test Club"));
            }
        }

        [Test]
        public void CanManageClubGroup()
        {
            var options = CreateNewContextOptions();
            using (var context = new ClubcoreDbContext(options))
            {
                var group1 = new Group { Name = "Group 1" };
                var group2 = new Group { Name = "Group 2" };
                var group3 = new Group { Name = "Group 3" };

                var club1 = new Club { Name = "Club 1" };
                var club2 = new Club { Name = "Club 2" };

                club1.Groups.Add(group1);
                club1.Groups.Add(group2);
                club1.Groups.Add(group3);

                club2.Groups.Add(group3);

                context.Clubs.AddRange(club1, club2);
                context.Groups.AddRange(group1, group2, group3);
                context.SaveChanges();
            }

            using (var context = new ClubcoreDbContext(options))
            {
                var group1 = context.Groups.Include(g => g.Clubs).Single(g => g.Name == "Group 1");
                var group2 = context.Groups.Include(g => g.Clubs).Single(g => g.Name == "Group 2");
                var group3 = context.Groups.Include(g => g.Clubs).Single(g => g.Name == "Group 3");

                Assert.That(group1.Clubs.Count, Is.EqualTo(1));
                Assert.That(group1.Clubs.Any(c => c.Name == "Club 1"), Is.True);

                Assert.That(group2.Clubs.Count, Is.EqualTo(1));
                Assert.That(group2.Clubs.Any(c => c.Name == "Club 1"), Is.True);

                Assert.That(group3.Clubs.Count, Is.EqualTo(2));
                Assert.That(group3.Clubs.Any(c => c.Name == "Club 1"), Is.True);
                Assert.That(group3.Clubs.Any(c => c.Name == "Club 2"), Is.True);


                var club1 = context.Clubs.Include(c => c.Groups).Single(c => c.Name == "Club 1");
                var club2 = context.Clubs.Include(c => c.Groups).Single(c => c.Name == "Club 2");

                Assert.That(club1.Groups.Count, Is.EqualTo(3));
                Assert.That(club1.Groups.Any(g => g.Name == "Group 1"), Is.True);
                Assert.That(club1.Groups.Any(g => g.Name == "Group 2"), Is.True);
                Assert.That(club1.Groups.Any(g => g.Name == "Group 3"), Is.True);

                Assert.That(club2.Groups.Count, Is.EqualTo(1));
                Assert.That(club2.Groups.Any(g => g.Name == "Group 3"), Is.True);

                group3.Clubs.Remove(club1);
                club1.Groups.Remove(group2);
                context.SaveChanges();
            }

            using (var context = new ClubcoreDbContext(options))
            {
                var group1 = context.Groups.Include(g => g.Clubs).Single(g => g.Name == "Group 1");
                var group2 = context.Groups.Include(g => g.Clubs).Single(g => g.Name == "Group 2");
                var group3 = context.Groups.Include(g => g.Clubs).Single(g => g.Name == "Group 3");

                Assert.That(group1.Clubs.Count, Is.EqualTo(1));
                Assert.That(group1.Clubs.Any(c => c.Name == "Club 1"), Is.True);

                Assert.That(group2.Clubs.Count, Is.EqualTo(0));

                Assert.That(group3.Clubs.Count, Is.EqualTo(1));
                Assert.That(group3.Clubs.Any(c => c.Name == "Club 2"), Is.True);


                var club1 = context.Clubs.Include(c => c.Groups).Single(c => c.Name == "Club 1");
                var club2 = context.Clubs.Include(c => c.Groups).Single(c => c.Name == "Club 2");

                Assert.That(club1.Groups.Count, Is.EqualTo(1));
                Assert.That(club1.Groups.Any(g => g.Name == "Group 1"), Is.True);

                Assert.That(club2.Groups.Count, Is.EqualTo(1));
                Assert.That(club2.Groups.Any(g => g.Name == "Group 3"), Is.True);

            }


        }

        [Test]
        public void CanManageGroupHierarchy()
        {
            var options = CreateNewContextOptions();

            // Run the test against one instance of the context
            using (var context = new ClubcoreDbContext(options))
            {
                var parentGroup1 = new Group { Name = "Parent Group 1" };
                var parentGroup2 = new Group { Name = "Parent Group 2" };
                var childGroup1 = new Group { Name = "Child Group 1" };
                var childGroup2 = new Group { Name = "Child Group 2" };

                parentGroup1.ChildGroups.Add(childGroup1);
                parentGroup2.ChildGroups.Add(childGroup1);
                parentGroup1.ChildGroups.Add(childGroup2);

                context.Groups.AddRange(parentGroup1, parentGroup2);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ClubcoreDbContext(options))
            {
                var parentGroup1 = context.Groups.Include(g => g.ChildGroups).Single(g => g.Name == "Parent Group 1");
                var parentGroup2 = context.Groups.Include(g => g.ChildGroups).Single(g => g.Name == "Parent Group 2");

                Assert.That(parentGroup1.ChildGroups.Count, Is.EqualTo(2));
                Assert.That(parentGroup1.ChildGroups.Any(gr => gr.Name == "Child Group 1"), Is.True);
                Assert.That(parentGroup1.ChildGroups.Any(gr => gr.Name == "Child Group 2"), Is.True);

                Assert.That(parentGroup2.ChildGroups.Count, Is.EqualTo(1));
                Assert.That(parentGroup2.ChildGroups.Any(gr => gr.Name == "Child Group 1"), Is.True);

                var childGroup1 = context.Groups.Include(g => g.ParentGroups).Single(g => g.Name == "Child Group 1");
                var childGroup2 = context.Groups.Include(g => g.ParentGroups).Single(g => g.Name == "Child Group 2");

                Assert.That(childGroup1.ParentGroups.Count, Is.EqualTo(2));
                Assert.That(childGroup1.ParentGroups.Any(gr => gr.Name == "Parent Group 1"), Is.True);
                Assert.That(childGroup1.ParentGroups.Any(gr => gr.Name == "Parent Group 2"), Is.True);

                Assert.That(childGroup2.ParentGroups.Count, Is.EqualTo(1));
                Assert.That(childGroup2.ParentGroups.Any(gr => gr.Name == "Parent Group 1"), Is.True);
            }
        }
    }
}