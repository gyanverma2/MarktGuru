using MarktGuru.Products.Application.Data.Products.Queries;
using MarktGuru.Products.Common.Enums;
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
    }
}
