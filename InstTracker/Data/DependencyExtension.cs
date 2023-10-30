using Microsoft.Extensions.DependencyInjection;

namespace InstTracker.Data
{
    internal static class DependencyExtension
    {
        public static IServiceProvider AddDbInitializer(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                throw;
            }

            return services;
        }
    }
}