namespace Vendora.Application.Common;

using Microsoft.EntityFrameworkCore;
using Vendora.Domain.Entities;

public interface IAppDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}