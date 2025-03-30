using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Clubcore.Entities;
using Microsoft.Extensions.Hosting;

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
        }
    }
}
