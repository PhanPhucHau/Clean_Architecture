using Clean_Architecture.Infrastructure.Persistence.EntityConfigurations.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitechtureDemo.Infrastructure.Persistence;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{

  //  private readonly IEnumerable<ISaveChangesInterceptor> _interceptors;
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    //  public DbSet<Project> Projects { get; set; }


    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {

    }

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //foreach (var interceptor in _interceptors)
        //{
        //    optionsBuilder.AddInterceptors(interceptor);
        //}
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
