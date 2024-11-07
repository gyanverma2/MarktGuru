using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Domain.Models;
using MarktGuru.Products.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Handlers.Products.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public List<ProductPriceRequest>? Price { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductDbContext _productDbContext;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateProductCommandHandler(IProductDbContext productDbContext, ILogger<CreateProductCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _productDbContext = productDbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateProductCommandHandler: Creating product with name: {ProductName}.", request.Name);
            if (IsDuplicateProductNames(_productDbContext, request.Name))
            {
                _logger.LogWarning("CreateProductCommandHandler: Product with name {ProductName} already exists.", request.Name);
                throw new ProductException($"Product with name {request.Name} already exists.");
            }

            using var transaction = await _productDbContext.DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                if (userName == null)
                {
                    _logger.LogError("CreateProductCommandHandler: User not found.");
                    throw new ProductException("User not found.");
                }
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    IsAvailable = request.IsAvailable,
                    CreatedBy = userName
                };

                _productDbContext.Products.Add(product);
                await _productDbContext.SaveChangesAsync(cancellationToken);
                if (request.Price != null)
                {
                    foreach (var price in request.Price)
                    {
                        var productPrice = new ProductPrice
                        {
                            ProductId = product.Id,
                            AmountExclTax = price.AmountExclTax,
                            TaxPercentage = price.TaxPercentage,
                            SourceTypeId = price.SourceTypeId,
                            BeginDate = price.BeginDate,
                            IsApproved = price.IsApproved,
                            EndDate = price.EndDate,
                            CreatedBy = userName
                        };
                        _productDbContext.ProductPrice.Add(productPrice);
                    }

                    await _productDbContext.SaveChangesAsync(cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("CreateProductCommandHandler: Product created successfully with ID: {ProductId}.", product.Id);
                return product;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "CreateProductCommandHandler: Error creating product: {Message}", ex.Message);
                throw;
            }
        }
        private static bool IsDuplicateProductNames(IProductDbContext productDbContext, string name)
        {
            return productDbContext.Products.Any(p => p.Name == name);
        }
    }
}
