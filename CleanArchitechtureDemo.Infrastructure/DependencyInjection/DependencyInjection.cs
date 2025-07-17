using CleanArchitechtureDemo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitechtureDemo.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // register connect database service
        services.AddDbContext<ApplicationDBContext>(options =>
        options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"),
            npgsql => npgsql.MigrationsAssembly(
                typeof(ApplicationDBContext).Assembly.FullName)));
        return services;
    }
}
