using MarktGuru.Products.Application.Handlers.Products.Commands;
using MarktGuru.Products.Application.Managers.Products;
using MediatR;
using Moq;
using Xunit;

namespace MarktGuru.Products.Application.Test.Handlers.Products.Commands
{
    public class DeleteProductCommandTest
    {
        [Fact]
        public async Task DeleteProductCommandTest_Success()
        {
            var mediator = new Mock<IMediator>();
            var id = 1;
            mediator.Setup(x => x.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            var manager = new ProductManager(mediator.Object);

            var result = await manager.DeleteProductAsync(id);

            Assert.True(result);
        }
    }
}
