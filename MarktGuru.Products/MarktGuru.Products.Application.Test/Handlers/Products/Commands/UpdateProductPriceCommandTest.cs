using MarktGuru.Products.Application.Handlers.Products.Commands;
using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Domain.Models;
using MediatR;
using Moq;
using Xunit;

namespace MarktGuru.Products.Application.Test.Handlers.Products.Commands
{
    public class UpdateProductPriceCommandTest
    {
        [Fact]
        public async Task UpdateProductPriceCommandTest_Success()
        {
            var mediator = new Mock<IMediator>();
            var productPrice = new ProductPrice
            {
                Id = 1,
                ProductId = 1,
                AmountExclTax = 100,
                TaxPercentage = 10,
                SourceTypeId = SourceTypeId.Web,
                BeginDate = DateTime.Now,
                EndDate = null,
                IsApproved = true
            };
            mediator.Setup(x => x.Send(It.IsAny<UpdateProductPriceCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(productPrice);
            var manager = new ProductManager(mediator.Object);

            var result = await manager.UpdateProductPrice(new UpdateProductPriceCommand());

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(100, result.AmountExclTax);
            Assert.Equal(10, result.TaxPercentage);
            Assert.Equal(SourceTypeId.Web, result.SourceTypeId);
            Assert.True(result.IsApproved);
        }
    }
}
