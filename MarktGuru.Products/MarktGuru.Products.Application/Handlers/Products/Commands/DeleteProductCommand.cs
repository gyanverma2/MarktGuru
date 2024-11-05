using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Handlers.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public DeleteProductCommand(int id)
        {
            ProductId = id;
        }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductDbContext _productDbContext;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteProductCommandHandler(IProductDbContext productDbContext, ILogger<DeleteProductCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _productDbContext = productDbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteProductCommandHandler: Deleting product with id: {ProductId}.", request.ProductId);
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (userName == null)
            {
                _logger.LogError("DeleteProductCommandHandler: User not found.");
                throw new ProductException("User not found.");
            }

            var product = await _productDbContext.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                _logger.LogWarning("DeleteProductCommandHandler: Product with id {ProductId} not found.", request.ProductId);
                throw new ProductException($"Product with id {request.ProductId} not found.");
            }

            using var transaction = await _productDbContext.DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var productPrices = _productDbContext.ProductPrice.Where(x => x.ProductId == request.ProductId);
                _productDbContext.ProductPrice.RemoveRange(productPrices);
                _productDbContext.Products.Remove(product);
                await _productDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteProductCommandHandler: Error deleting product with id: {ProductId}.", request.ProductId);
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
