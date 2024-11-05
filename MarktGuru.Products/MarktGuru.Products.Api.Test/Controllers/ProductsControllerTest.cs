using MarktGuru.Products.Api.Controllers;
using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarktGuru.Products.Api.Test.Controllers
{
    public class ProductsControllerTest
    {
        [Fact]
        public async Task GetProductsAsync_SuccessTest()
        {
            var mockProductManager = new Mock<IProductManager>();
            var mockLogger = new Mock<ILogger<ProductsController>>();
            mockProductManager.Setup(x => x.GetProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(PaginatedResult<Product>.Success(new List<Product>() { new Product()}, 0, 0, 0));

            var controller = new ProductsController(mockProductManager.Object, mockLogger.Object);
            var result = await controller.GetAll(1, 10);

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetProductByIdAsync_SuccessTest()
        {
            var mockProductManager = new Mock<IProductManager>();
            var mockLogger = new Mock<ILogger<ProductsController>>();
            mockProductManager.Setup(x => x.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Product());

            var controller = new ProductsController(mockProductManager.Object, mockLogger.Object);
            var result = await controller.GetById(1);

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
        }
    }
}
