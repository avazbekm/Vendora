namespace Vendora.Infrastructure.Persistence.EntityFramwork;

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

        builder.Property(u => u.Patronomyc)
               .HasMaxLength(30)
               .IsRequired(false);

        builder.Property(u => u.Login)
               .HasMaxLength(30)
               .IsRequired();

        builder.Property(u => u.Password)
               .HasMaxLength(256) // Hashed passwords uchun uzunroq uzunlik
               .IsRequired();

        builder.Property(u => u.PasportSeria)
               .HasMaxLength(9) // O‘zbek pasport seriyasi uchun (AA1234567)
               .IsRequired(false);

        builder.Property(u => u.Phone)
               .HasMaxLength(13) // +998901234567 format
               .IsRequired();

        builder.Property(u => u.DateOfBirth)
               .IsRequired(false)
               .HasColumnType("timestamp with time zone");

        builder.Property(u => u.Gender)
               .IsRequired()
               .HasConversion<string>(); // Enum’ni string sifatida saqlash

        builder.Property(u => u.RoleId)
               .IsRequired();

        builder.Property(u => u.PhotoId)
               .IsRequired(false); // Ixtiyoriy, chunki nullable

        // Indexes
        builder.HasIndex(u => u.Login)
               .IsUnique(); // Login unik bo‘lishi kerak

        // Relationships
        // Add relationships here if needed
    }
}
