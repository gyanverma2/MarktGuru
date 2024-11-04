using MarktGuru.Products.Common.Wrapper;
using Xunit;

namespace MarketGuru.Products.Common.Test.Wrapper
{
    public class PaginatedResultTest
    {
        [Fact]
        public void Constructor_Test()
        {
            var items = new List<string> { "item1", "item2" };
            var paginatedResult = PaginatedResult<string>.Success(items,items.Count,1,10);

            Assert.Equal(items, paginatedResult.Data);
            Assert.Equal(items.Count, paginatedResult.TotalCount);
            Assert.Equal(1, paginatedResult.CurrentPage);
            Assert.Equal(1, paginatedResult.TotalPages);
            Assert.Equal(10, paginatedResult.PageSize);
            Assert.False(paginatedResult.HasNextPage);
            Assert.False(paginatedResult.HasPreviousPage);
        }
        [Fact]
        public void Failure_Test()
        {
            var messages = new List<string> { "message1", "message2" };
            var paginatedResult = PaginatedResult<string>.Failure(messages);

            Assert.Null(paginatedResult.Data);
            Assert.Equal(messages, paginatedResult.Messages);
            Assert.False(paginatedResult.Succeeded);
        }
    }
}
