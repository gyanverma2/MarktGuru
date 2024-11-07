using MarktGuru.Products.Common.Enums;

namespace MarktGuru.Products.Application.Handlers.Products.Commands
{
    public class ProductPriceRequest
    {
        public decimal AmountExclTax { get; set; }
        public decimal TaxPercentage { get; set; }
        public SourceTypeId SourceTypeId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
