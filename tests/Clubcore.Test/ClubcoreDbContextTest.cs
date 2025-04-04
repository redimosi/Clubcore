using Clubcore.Domain.AggregatesModel;
using Clubcore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clubcore.Tests
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

        [Test]
        public void CanManagePersonRoles()
        {
            var options = CreateNewContextOptions();
            // Run the test against one instance of the context
            using (var context = new ClubcoreDbContext(options))
            {
                var person1 = new Person { Name = new PersonName { FirstName = "Test", LastName = "Person 1" } };
                var person2 = new Person { Name = new PersonName { FirstName = "Test", LastName = "Person 2" } };
                var role1 = new Role { Name = "Role 1" };
                var role2 = new Role { Name = "Role 2" };
                person1.Roles.Add(role1);
                person1.Roles.Add(role2);
                person2.Roles.Add(role2);
                context.Persons.AddRange(person1, person2);
                context.Roles.AddRange(role1, role2);
                context.SaveChanges();
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ClubcoreDbContext(options))
            {
                var person1 = context.Persons.Include(p => p.Roles).Single(p => p.Name.FirstName == "Test" && p.Name.LastName == "Person 1");
                var person2 = context.Persons.Include(p => p.Roles).Single(p => p.Name.FirstName == "Test" && p.Name.LastName == "Person 2");
                Assert.That(person1.Roles.Count, Is.EqualTo(2));
                Assert.That(person1.Roles.Any(r => r.Name == "Role 1"), Is.True);
                Assert.That(person1.Roles.Any(r => r.Name == "Role 2"), Is.True);
                Assert.That(person2.Roles.Count, Is.EqualTo(1));
                Assert.That(person2.Roles.Any(r => r.Name == "Role 2"), Is.True);
                var role1 = context.Roles.Include(r => r.Persons).Single(r => r.Name == "Role 1");
                var role2 = context.Roles.Include(r => r.Persons).Single(r => r.Name == "Role 2");
                Assert.That(role1.Persons.Count, Is.EqualTo(1));
                Assert.That(role1.Persons.Any(p => p.Name.FirstName == "Test" && p.Name.LastName == "Person 1"), Is.True);
                Assert.That(role2.Persons.Count, Is.EqualTo(2));
                Assert.That(role2.Persons.Any(p => p.Name.FirstName == "Test" && p.Name.LastName == "Person 1"), Is.True);
            }
        }

        [Test]
        public void CanManageGroupPersonRoles()
        {
            var options = CreateNewContextOptions();
            // Run the test against one instance of the context
            using (var context = new ClubcoreDbContext(options))
            {
                var person1 = new Person { Name = new PersonName { FirstName = "Test", LastName = "Person 1" } };
                var person2 = new Person { Name = new PersonName { FirstName = "Test", LastName = "Person 2" } };
                var group1 = new Group { Name = "Group 1" };
                var group2 = new Group { Name = "Group 2" };
                var role1 = new Role { Name = "Role 1" };
                var role2 = new Role { Name = "Role 2" };
                person1.Roles.Add(role1);
                person1.Roles.Add(role2);
                person2.Roles.Add(role2);
                context.Persons.AddRange(person1, person2);
                context.Roles.AddRange(role1, role2);
                context.Groups.AddRange(group1, group2);
                context.SaveChanges();
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ClubcoreDbContext(options))
            {
                var person1 = context.Persons.Include(p => p.Roles).Single(p => p.Name.FirstName == "Test" && p.Name.LastName == "Person 1");
                var person2 = context.Persons.Include(p => p.Roles).Single(p => p.Name.FirstName == "Test" && p.Name.LastName == "Person 2");
                Assert.That(person1.Roles.Count, Is.EqualTo(2));
                Assert.That(person1.Roles.Any(r => r.Name == "Role 1"), Is.True);
                Assert.That(person1.Roles.Any(r => r.Name == "Role 2"), Is.True);
                Assert.That(person2.Roles.Count, Is.EqualTo(1));
                Assert.That(person2.Roles.Any(r => r.Name == "Role 2"), Is.True);
                var role1 = context.Roles.Include(r => r.Persons).Single(r => r.Name == "Role 1");
                var role2 = context.Roles.Include(r => r.Persons).Single(r => r.Name == "Role 2");
            }
        }
    }
}