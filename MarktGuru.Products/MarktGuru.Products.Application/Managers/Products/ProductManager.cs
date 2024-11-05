using MarktGuru.Products.Application.Data.Products.Queries;
using MarktGuru.Products.Application.Handlers.Products.Commands;
using MarktGuru.Products.Application.Handlers.Products.Queries;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;
using MediatR;

namespace MarktGuru.Products.Application.Managers.Products
{
    public class ProductManager : IProductManager
    {
        private readonly IMediator _mediator;
        public ProductManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<PaginatedResult<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetProductQuery(pageNumber, pageSize));
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _mediator.Send(new GetProductByIdQuery(id));
        }
        public async Task<Product> CreateProductAsync(CreateProductCommand product)
        {
            return await _mediator.Send(product);
        }
    }
}
