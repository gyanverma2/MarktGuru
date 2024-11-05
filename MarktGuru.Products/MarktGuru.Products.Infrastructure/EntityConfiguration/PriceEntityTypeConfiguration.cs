using MarktGuru.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace MarktGuru.Products.Infrastructure.EntityConfiguration
{
    [ExcludeFromCodeCoverage]
    public class PriceEntityTypeConfiguration : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.AmountExclTax).IsRequired();
            builder.Property(p => p.TaxPercentage).IsRequired();
            builder.Property(p => p.SourceTypeId).HasColumnType("smallint");
            builder.Property(p => p.BeginDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.EndDate);
            builder.Property(p => p.IsApproved).HasDefaultValue(false);
            builder.Property(p => p.ProductId).IsRequired();

            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ModifiedBy).HasMaxLength(100);
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.ModifiedAt);

            builder.HasIndex(p => new { p.ProductId, p.BeginDate, p.SourceTypeId }).IsUnique();
            
            builder.ToTable("ProductPrices", t =>
            {
                t.HasCheckConstraint("CK_Price_BeginDate_EndDate", "EndDate IS NULL OR BeginDate < EndDate");
            });
        }
    }
}
