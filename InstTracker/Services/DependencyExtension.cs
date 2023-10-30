using InstTracker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InstTracker.Services
{
    internal static class DependencyExtension
    {
        internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IInstanceService, InstanceService>();
            services.AddScoped<ICronService, CronService>();
            services.AddScoped<IHistoryService, HistoryService>();

            return services;
        }
    }
}