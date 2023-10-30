using InstTracker.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace InstTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<InstanceHistory> InstancesHistory { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<Character>()
                .HasMany(x => x.InstancesHistory)
                .WithOne(x => x.Character)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Instance>()
                .HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<Instance>()
                .HasMany(x => x.InstancesHistory)
                .WithOne(x => x.Instance)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}