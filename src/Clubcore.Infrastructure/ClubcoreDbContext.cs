using Clubcore.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Infrastructure
{
    public class ClubcoreDbContext(DbContextOptions<ClubcoreDbContext> options) : DbContext(options)
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Groups)
                .WithMany(g => g.Clubs)
                .UsingEntity<ClubGroup>();

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Clubs)
                .WithMany(c => c.Groups)
                .UsingEntity<ClubGroup>();

            modelBuilder.Entity<Group>()
                .HasMany(g => g.ParentGroups)
                .WithMany(g => g.ChildGroups)
                .UsingEntity<GroupRelationship>(
                    l => l.HasOne<Group>().WithMany().HasForeignKey(e => e.ParentGroupId),
                    r => r.HasOne<Group>().WithMany().HasForeignKey(e => e.ChildGroupId));

            modelBuilder.Entity<Person>()
                .OwnsOne(p => p.Name);

            modelBuilder.Entity<Person>()
                .HasMany(g => g.Roles)
                .WithMany(c => c.Persons)
                .UsingEntity<PersonRole>();

            modelBuilder.Entity<Role>()
                .HasMany(g => g.Persons)
                .WithMany(c => c.Roles)
                .UsingEntity<PersonRole>();

            modelBuilder.Entity<Event>()
            .HasMany(e => e.Feedbacks)
            .WithOne()
            .HasForeignKey(f => f.EventId);
        }
    }
}

