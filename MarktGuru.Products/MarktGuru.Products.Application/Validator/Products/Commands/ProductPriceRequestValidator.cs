using FluentValidation;
using MarktGuru.Products.Application.Handlers.Products.Commands;

namespace MarktGuru.Products.Application.Validator.Products.Commands
{
    public class ProductPriceRequestValidator : AbstractValidator<ProductPriceRequest>
    {
        public ProductPriceRequestValidator()
        {
            RuleFor(x => x.AmountExclTax).NotEmpty().WithMessage("Price is required.");
            RuleFor(x => x.TaxPercentage).LessThan(100).WithMessage("Tax percentage must not be 100%");
            RuleFor(x => x.BeginDate).NotEmpty().WithMessage("Begin date is required.");
        }
    }
}
