namespace Vendora.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendora.Domain.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name
        builder.ToTable("Users");

        // Primary key
        builder.HasKey(u => u.Id);

        // Properties
        builder.Property(u => u.FirstName)
               .HasMaxLength(30);

        builder.Property(u => u.LastName)
               .HasMaxLength(30)
               .IsRequired(false);

        builder.Property(u => u.Username)
               .HasMaxLength(22);

        builder.Property(u => u.Password)
               .IsRequired();

        builder.Property(u => u.PhotoId)
               .IsRequired();

        // Relationships
        // Add relationships here if needed
    }
}
