using MarktGuru.Products.Application.Handlers.Products.Queries;
using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Domain.Models;
using MediatR;
using Moq;
using Xunit;

namespace MarktGuru.Products.Application.Test.Handlers.Products.Queries
{
    public class GetProductByIdQueryTest
    {
        private const int Id = 1;
        private const string Name = "Product 1";
        private const string Description = "Description 1";
        private const bool IsAvailable = true;
        [Fact]
        public async Task GetProductByIdQueryTest_Success()
        {
            var mediator = new Mock<IMediator>();
            var product = new Product
            {
                Id = Id,
                Name = Name,
                Description =Description,
                IsAvailable =IsAvailable
            };
            mediator.Setup(x => x.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var manager = new ProductManager(mediator.Object);

            var result = await manager.GetProductByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(Id, result.Id);
            Assert.Equal(Name, result.Name);
            Assert.Equal(Description, result.Description);
            Assert.Equal(IsAvailable, result.IsAvailable);
        }
    }
}
