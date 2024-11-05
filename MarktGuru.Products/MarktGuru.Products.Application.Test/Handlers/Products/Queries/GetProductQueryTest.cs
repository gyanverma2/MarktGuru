using MarktGuru.Products.Application.Data.Products.Queries;
using MarktGuru.Products.Application.Test.Mock;
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

    }
}
