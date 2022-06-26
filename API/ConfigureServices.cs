using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class ConfigureServices
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationContext>(o =>
                o.UseNpgsql(
                    config.GetConnectionString("Production")));
        }

        // Repository wrapper is instantiated as singleton to prevent it from checking whether the database is created on each request
        public static void ConfigureRepositoryWrapper(this IServiceCollection services) => services.AddScoped<RepositoryWrapper>();
    }
}