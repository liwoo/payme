using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<PayMeContext>(options =>
                    options.UseSqlite("Data Source=payme.db",
                    b => b.MigrationsAssembly(typeof(PayMeContext).Assembly.FullName))
            );

            return services;
        }
    }
}