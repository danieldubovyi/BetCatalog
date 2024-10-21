using BetCatalog.Infrastructure.EFCore.Repositories;
using BetCatalog.Models.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BetCatalog.Infrastructure.EFCore
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            //services.AddTransient<IEntityRepository<Team>, TeamRepository>();
            return services;
        }
    }
}
