namespace Vendora.Infrastructure.Persistence.EntityFramwork;

using Microsoft.EntityFrameworkCore;
using Vendora.Application.Interfaces;
using Vendora.Domain.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IAppDbContext
{
    public required DbSet<User> Users { get; set; }
    public required DbSet<Role> Roles { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Product> Products { get; set; }
    public required DbSet<ProductPhoto> ProductPhotos { get; set; }
    public required DbSet<Asset> Assets { get; set; }
    public required DbSet<Measure> Measures { get; set; }
    public required DbSet<Partner> Partners { get; set; }
    public required DbSet<PartnerDetail> PartnerDetails { get; set; }
    public required DbSet<Supply> Supplies { get; set; }
    public required DbSet<SupplyItem> SupplyItems { get; set; }
    public required DbSet<Stocks> Stocks { get; set; }
    public required DbSet<Warehouse> Warehouses { get; set; }
    public required DbSet<Sale> Sales { get; set; }
    public required DbSet<SaleItem> SaleItems { get; set; }
    public required DbSet<Currency> Currencies { get; set; }
    public required DbSet<Transaction> Transactions { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}