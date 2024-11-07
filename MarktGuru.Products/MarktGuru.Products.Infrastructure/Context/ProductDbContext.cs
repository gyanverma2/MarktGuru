using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MarktGuru.Products.Infrastructure.Context
{
    [ExcludeFromCodeCoverage]
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        public DbContext DbContext => this;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.ApplyConfiguration(new SourceTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PriceEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => base.SaveChangesAsync(cancellationToken);
       
    }
}
