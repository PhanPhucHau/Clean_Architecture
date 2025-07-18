using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean_Architecture.Infrastructure.DependencyInjection;

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

        services.AddScoped<IApplicationDbContext>(provider => (IApplicationDbContext)provider.GetRequiredService<ApplicationDBContext>());

        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //// register repositories
        //services.AddScoped<IProjectRepository, ProjectRepository>(); // Register specific repository for Project entity
        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Register generic repository for all entities
        return services;
    }
}
