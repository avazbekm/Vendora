using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendora.Domain.Entities;

namespace Vendora.Infrastructure.Persistence.EntityFramwork;


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Table name
        builder.ToTable("Categories");

        // Primary key
        builder.HasKey(u => u.Id);
    }
}
