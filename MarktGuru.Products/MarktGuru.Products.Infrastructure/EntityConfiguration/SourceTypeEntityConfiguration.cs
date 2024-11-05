using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MarktGuru.Products.Common.Extensions;
using System.Diagnostics.CodeAnalysis;
namespace MarktGuru.Products.Infrastructure.EntityConfiguration
{
    [ExcludeFromCodeCoverage]
    public class SourceTypeEntityConfiguration : IEntityTypeConfiguration<SourceType>
    {
        public void Configure(EntityTypeBuilder<SourceType> builder)
        {
            builder.Property(x => x.Id)
                    .HasConversion<int>()
                    .HasColumnType("smallint")
                    .ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(Enum.GetValues<SourceTypeId>()
                .Select(e => new SourceType() { Id = e, Name = e.GetDisplayName() }));
            builder.ToTable("SourceTypes");

        }
    }
}
