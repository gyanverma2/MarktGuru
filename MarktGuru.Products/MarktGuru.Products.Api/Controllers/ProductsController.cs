using MarktGuru.Products.Api.Constants;
using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarktGuru.Products.Api.Controllers
{
    /// <summary>
    /// Products Api
    /// </summary>
    [Route(ApiRoutes.BaseV1)]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductManager _productManager;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productManager"></param>
        /// <param name="logger"></param>
        public ProductsController(IProductManager productManager, ILogger<ProductsController> logger)
        {
            _productManager = productManager;
            _logger = logger;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="pageNumber">1</param>
        /// <param name="pageSize">20</param>
        /// <returns>A list of products</returns>
        /// <response code="200">Returns the list of products</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedResult<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber=1, [FromQuery] int pageSize=20)
        {
            _logger.LogInformation("Getting all products");
            if (pageNumber < 1 || pageSize < 1)
            {
                _logger.LogError("Invalid page number or page size");
                return BadRequest("Invalid page number or page size");
            }
            var products = await _productManager.GetProductsAsync(pageNumber, pageSize);
            _logger.LogInformation("Returning all products");
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting product by id");
            var product = await _productManager.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogError("Product not found");
                return NotFound();
            }
            _logger.LogInformation("Returning product by id");
            return Ok(product);
        }

    }
}
