using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Infrastructure.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Handlers.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductDbContext _productDbContext;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(IProductDbContext productDbContext, ILogger<GetProductByIdQueryHandler> logger)
        {
            _productDbContext = productDbContext;
            _logger = logger;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductByIdQueryHandler: Getting product by id");
            var product = await _productDbContext.Products.FindAsync(request.Id);
            if (product == null)
            {
                _logger.LogWarning("GetProductByIdQueryHandler: Product not found");
                throw new RecordNotFoundException();
            }
            _logger.LogInformation("GetProductByIdQueryHandler: Returning product by id");
            return product;
        }
    }
}
