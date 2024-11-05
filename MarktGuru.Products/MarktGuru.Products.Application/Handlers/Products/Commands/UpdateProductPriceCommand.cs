using MarktGuru.Products.Common.Enums;
using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Handlers.Products.Commands
{
    public class UpdateProductPriceCommand : IRequest<ProductPrice>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal AmountExclTax { get; set; }
        public decimal TaxPercentage { get; set; }
        public SourceTypeId SourceTypeId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsApproved { get; set; }
    }
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, ProductPrice>
    {
        private readonly IProductDbContext _productDbContext;
        private readonly ILogger<UpdateProductPriceCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateProductPriceCommandHandler(IProductDbContext productDbContext, ILogger<UpdateProductPriceCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _productDbContext = productDbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductPrice> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateProductPriceCommandHandler: Updating product price with id: {ProductPriceId}.", request.Id);
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (userName == null)
            {
                _logger.LogError("UpdateProductPriceCommandHandler: User not found.");
                throw new ProductException("User not found.");
            }
            
            var productPrice = await _productDbContext.ProductPrice.FindAsync(request.Id);
            if (productPrice == null)
            {
                _logger.LogWarning("UpdateProductPriceCommandHandler: Product price with id {ProductPriceId} not found.", request.Id);
                throw new ProductException($"Product price with id {request.Id} not found.");
            }


            using var transaction = await _productDbContext.DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                productPrice.AmountExclTax = request.AmountExclTax;
                productPrice.TaxPercentage = request.TaxPercentage;
                productPrice.SourceTypeId = request.SourceTypeId;
                productPrice.BeginDate = request.BeginDate;
                productPrice.EndDate = request.EndDate;
                productPrice.IsApproved = request.IsApproved; 
                _productDbContext.ProductPrice.Update(productPrice);
                await _productDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("UpdateProductPriceCommandHandler: Product price with id {ProductPriceId} updated successfully.", request.Id);
                return productPrice;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "UpdateProductPriceCommandHandler: Error updating product with id {ProductId}.", request.Id);
                throw new ProductException($"Error updating product price with id {request.Id}.", ex);
            }
        }
    }
}
