using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Infrastructure.Persistence.EntityConfigurations.Models;
using CleanArchitechtureDemo.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitechtureDemo.Infrastructure.Persistence;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
{
    private readonly IEnumerable<ISaveChangesInterceptor> _interceptors;
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Filter> Filters { get; set; }
    public DbSet<History> Histories { get; set; }
    public DbSet<Notify> Notifies { get; set; }
    public DbSet<User> Users { get; set; }


    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IEnumerable<ISaveChangesInterceptor> interceptors)
        : base(options)
    {
        _interceptors = interceptors;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        foreach (var interceptor in _interceptors)
        {
            optionsBuilder.AddInterceptors(interceptor);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
