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
            base.OnModelCreating(modelBuilder);
        }
    }
}
