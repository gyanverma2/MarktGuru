using MarktGuru.Products.Application.Handlers.Products.Commands;
using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Domain.Models;
using MediatR;
using Moq;
using Xunit;

namespace MarktGuru.Products.Application.Test.Handlers.Products.Commands
{
    public class UpdateProductCommandTest
    {
        [Fact]
        public async Task UpdateProductCommandTest_Success()
        {
            var mediator = new Mock<IMediator>();
            var product = new Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                IsAvailable = true
            };
            mediator.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var manager = new ProductManager(mediator.Object);

            var result = await manager.UpdateProductAsync(new UpdateProductCommand());

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Product 1", result.Name);
            Assert.Equal("Description 1", result.Description);
            Assert.True(result.IsAvailable);
        }
    }
}
