namespace Vendora.Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using Vendora.Domain.Entities;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductPhoto> ProductPhotos { get; }
    DbSet<Asset> Assets { get; }
    DbSet<Measure> Measures { get; }
    DbSet<Partner> Partners { get; }
    DbSet<PartnerDetail> PartnerDetails { get; }
    DbSet<Supply> Supplies { get; }
    DbSet<SupplyItem> SupplyItems { get; }
    DbSet<Stocks> Stocks { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<Sale> Sales { get; }
    DbSet<SaleItem> SaleItems { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<Transaction> Transactions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}