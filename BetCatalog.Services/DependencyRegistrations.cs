using Microsoft.Extensions.DependencyInjection;

namespace BetCatalog.Services
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(DependencyRegistrations).Assembly));
            //services.AddFileServices();
            //services.AddMatchServices();
            return services;
        }
    }
}
