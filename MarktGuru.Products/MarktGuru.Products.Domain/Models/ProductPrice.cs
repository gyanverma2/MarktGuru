using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Domain.Shared;

namespace MarktGuru.Products.Domain.Models
{
    public class ProductPrice : Auditable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal AmountInclTax => AmountExclTax + (AmountExclTax * TaxPercentage / 100);
        public decimal AmountExclTax { get; set; }
        public decimal TaxPercentage { get; set; }
        public SourceTypeId SourceTypeId { get; set; } = SourceTypeId.Any;
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsApproved { get; set; }
        public Product Product { get; set; } = null!;
    }
}
