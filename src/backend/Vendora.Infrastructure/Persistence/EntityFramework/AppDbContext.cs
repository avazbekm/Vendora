namespace Vendora.Infrastructure.Persistence.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Vendora.Application.Common;
using Vendora.Domain.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IAppDbContext
{
    public required DbSet<User> Users { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}