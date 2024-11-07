using MarktGuru.Products.Application.Data.Products.Queries;
using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Application.Test.Mock;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarktGuru.Products.Application.Test.Handlers.Products.Queries
{
    public class GetProductQueryTest
    {
        [Fact]
        public void Handler_Constructor_Test()
        {
            var dbContext = MockHelper.MockDbContext();
            var logger = new Mock<ILogger<GetProductQueryHandler>>();
            var handler = new GetProductQueryHandler(dbContext.Object, logger.Object);
            Assert.NotNull(handler);
        }
        [Fact]
        public void GetProductQuery_Constructor_Test()
        {
            var query = new GetProductQuery(1, 10);
            Assert.NotNull(query);
            Assert.Equal(1, query.PageNumber);
            Assert.Equal(10, query.PageSize);
        }
        [Fact]
        public async Task GetProductsTest_SuccessAsync()
        {
            var mediator = new Mock<IMediator>();
            var products =  new List<Product>() { new Product { Id = 1, Name = "Product 1", Description = "Description 1" } };
            var paginatedResult = PaginatedResult<Product>.Success(products, 1, 1, 10);
            mediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(paginatedResult);
            var manager = new ProductManager(mediator.Object);

            var result = await manager.GetProductsAsync(1,10);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.TotalCount);
            Assert.Equal(1, result.Data[0].Id);
        }

    }
}
