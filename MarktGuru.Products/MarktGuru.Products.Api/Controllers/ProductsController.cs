using MarktGuru.Products.Api.Constants;
using MarktGuru.Products.Application.Handlers.Products.Commands;
using MarktGuru.Products.Application.Managers.Products;
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

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Return product based on provided id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting product by id");
            if (id < 1)
            {
                _logger.LogWarning("Invalid product id");
                return BadRequest("Invalid product id");
            }
            var product = await _productManager.GetProductByIdAsync(id);
            _logger.LogInformation("Returning product by id");
            return Ok(product);
        }
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return a product</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync(CreateProductCommand request)
        {
           _logger.LogInformation("Creating product");
            var result = await _productManager.CreateProductAsync(request);
            _logger.LogInformation("Product created successfully");
            return Ok(result);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return updated product</returns>
        [HttpPut("update")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductCommand request)
        {
            _logger.LogInformation("Updating product");
            var result = await _productManager.UpdateProductAsync(request);
            _logger.LogInformation("Product updated successfully");
            return Ok(result);
        }

        /// <summary>
        /// Update product price
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return updated price</returns>
        [HttpPut("update-price")]
        [ProducesResponseType(typeof(ProductPrice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductPrice(UpdateProductPriceCommand request)
        {
            _logger.LogInformation("Updating product price");
            var result = await _productManager.UpdateProductPrice(request);
            _logger.LogInformation("Product price updated successfully");
            return Ok(result);
        }

    }
}
