using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Clubcore.Entities;

namespace Clubcore.Infrastructure
{
    public class ClubcoreDbContext : DbContext
    {
        public ClubcoreDbContext(DbContextOptions<ClubcoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<GroupRelationship> GroupRelationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupRelationship>()
                .HasKey(gr => new { gr.ParentGroupId, gr.ChildGroupId });

            modelBuilder.Entity<GroupRelationship>()
                .HasOne(gr => gr.ParentGroup)
                .WithMany(g => g.ChildGroups)
                .HasForeignKey(gr => gr.ParentGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupRelationship>()
                .HasOne(gr => gr.ChildGroup)
                .WithMany(g => g.ParentGroups)
                .HasForeignKey(gr => gr.ChildGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
        }
    }
}
