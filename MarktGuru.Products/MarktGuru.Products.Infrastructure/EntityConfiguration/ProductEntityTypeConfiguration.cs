using MarktGuru.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace MarktGuru.Products.Infrastructure.EntityConfiguration
{
    [ExcludeFromCodeCoverage]
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.IsAvailable).IsRequired();
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ModifiedBy).HasMaxLength(100);
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.ModifiedAt);

            builder.HasIndex(p => p.Name).IsUnique();
            builder.ToTable("Products");
        }
    }
}
