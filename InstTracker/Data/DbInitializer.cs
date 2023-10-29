using InstTracker.Data.Model;

namespace InstTracker.Data
{
    internal static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Instances.Any())
            {
                return;
            }

            var instances = new List<Instance>()
            {
                new Instance() {Name = "Арена младших богов", Schedule = "* * * * *"},
                new Instance() {Name = "Истхина", Schedule = "* * * * *"}
            };

            context.Instances.AddRange(instances);
            context.SaveChanges();
        }
    }
}
