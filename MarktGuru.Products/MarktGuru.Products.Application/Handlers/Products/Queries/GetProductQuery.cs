using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Common.Wrapper;
using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Data.Products.Queries
{
    public class GetProductQuery : IRequest<PaginatedResult<Product>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetProductQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, PaginatedResult<Product>>
    {
        private readonly IProductDbContext _dbContext;
        private readonly ILogger<GetProductQueryHandler> _logger;
        public GetProductQueryHandler(IProductDbContext dbContext, ILogger<GetProductQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<PaginatedResult<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductQueryHandler: Getting all products");
            var count = await _dbContext.Products.CountAsync(cancellationToken: cancellationToken);
            if (count <= 0)
            {
                _logger.LogWarning("GetProductQueryHandler: No products found");
                return PaginatedResult<Product>.Failure(new List<string> { "No products found" });
            }
            var products = await _dbContext.Products.Include(p => p.Prices).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken: cancellationToken);
            return PaginatedResult<Product>.Success(products, count, request.PageNumber, request.PageSize);
        }
    }
}
