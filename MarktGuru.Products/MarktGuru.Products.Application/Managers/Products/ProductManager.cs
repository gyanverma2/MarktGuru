using MarktGuru.Products.Application.Data.Products.Queries;
using MarktGuru.Products.Application.Events;
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
            var result = await _mediator.Send(product);
            await _mediator.Publish(new CreateUpdateProductEvent(result.Id, result.Name));
            return result;
        }
        public async Task<Product> UpdateProductAsync(UpdateProductCommand product)
        {
            var result = await _mediator.Send(product);
            await _mediator.Publish(new CreateUpdateProductEvent(result.Id, result.Name));
            return result;
        }
        public async Task<ProductPrice> UpdateProductPrice(UpdateProductPriceCommand price)
        {
            return await _mediator.Send(price);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var result =  await _mediator.Send(new DeleteProductCommand(id));
            await _mediator.Publish(new DeleteProductEvent(id));
            return result;
        }
    }
}
