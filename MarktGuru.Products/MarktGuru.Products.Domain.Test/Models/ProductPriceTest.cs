using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Domain.Models;
using Xunit;

namespace MarktGuru.Products.Domain.Test.Models
{
    public class ProductPriceTest
    {
        private const int ProductId = 1;
        private const decimal AmountExclTax = 100.00m;
        private const decimal TaxPercentage = 0.2m;
        private readonly SourceTypeId SourceTypeId = SourceTypeId.Any;
        private readonly DateTime BeginDate = new(2021, 1, 1,0,0,0,DateTimeKind.Unspecified);
        private DateTime? EndDate = null;
        private readonly bool IsApproved = true;

        [Fact]
        public void Constructor_Test()
        {
            var productPrice = new ProductPrice()
            {
                ProductId = ProductId,
                AmountExclTax = AmountExclTax,
                TaxPercentage = TaxPercentage,
                SourceTypeId = SourceTypeId,
                BeginDate = BeginDate,
                EndDate = EndDate,
                IsApproved = IsApproved
            };

            Assert.Equal(ProductId, productPrice.ProductId);
            Assert.Equal(AmountExclTax, productPrice.AmountExclTax);
            Assert.Equal(TaxPercentage, productPrice.TaxPercentage);
            Assert.Equal(SourceTypeId, productPrice.SourceTypeId);
            Assert.Equal(BeginDate, productPrice.BeginDate);
            Assert.Equal(EndDate, productPrice.EndDate);
            Assert.Equal(IsApproved, productPrice.IsApproved);
            Assert.Equal(AmountExclTax + (AmountExclTax * TaxPercentage / 100), productPrice.AmountInclTax);
        }
    }
}
