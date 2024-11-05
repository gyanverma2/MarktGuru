using MarktGuru.Products.Infrastructure.Context;
using Moq;

namespace MarktGuru.Products.Application.Test.Mock
{
    public static class MockHelper
    {
        public static Mock<IProductDbContext> MockDbContext()
        {
            var dbContext = new Mock<IProductDbContext>();
            return dbContext;
        }
    }
}
