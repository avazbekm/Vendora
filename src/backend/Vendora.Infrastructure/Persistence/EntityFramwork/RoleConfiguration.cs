namespace Vendora.Infrastructure.Persistence.EntityFramwork;

using Microsoft.EntityFrameworkCore;
using global::Vendora.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Table name
        builder.ToTable("Roles");

        // Primary key
        builder.HasKey(r => r.Id);

        // Properties
        builder.Property(r => r.RoleName)
               .HasMaxLength(50)
               .IsRequired();
    }
}