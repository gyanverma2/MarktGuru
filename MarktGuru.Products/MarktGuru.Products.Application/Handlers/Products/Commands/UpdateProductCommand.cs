using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Handlers.Products.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductDbContext _productDbContext;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateProductCommandHandler(IProductDbContext productDbContext, ILogger<UpdateProductCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _productDbContext = productDbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateProductCommandHandler: Updating product with id: {ProductId}.", request.Id);
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (userName == null)
            {
                _logger.LogError("UpdateProductCommandHandler: User not found.");
                throw new ProductException("User not found.");
            }
            var product = _productDbContext.Products.FirstOrDefault(x => x.Id == request.Id);
            if (product == null)
            {
                _logger.LogWarning("UpdateProductCommandHandler: Product with id {ProductId} not found.", request.Id);
                throw new ProductException($"Product with id {request.Id} not found.");
            }
           
            using var transaction = await _productDbContext.DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.IsAvailable = request.IsAvailable;
                product.ModifiedBy = userName;
                product.ModifiedAt = DateTime.UtcNow;
                _productDbContext.Products.Update(product);
                await _productDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("UpdateProductCommandHandler: Product with id {ProductId} updated successfully.", request.Id);
                return product;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "UpdateProductCommandHandler: Error updating product with id {ProductId}.", request.Id);
                throw new ProductException($"Error updating product with id {request.Id}.", ex);
            }

        }
    }
}
