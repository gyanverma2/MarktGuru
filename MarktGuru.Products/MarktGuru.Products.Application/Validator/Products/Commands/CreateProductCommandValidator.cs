using FluentValidation;
using MarktGuru.Products.Application.Handlers.Products.Commands;

namespace MarktGuru.Products.Application.Validator.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Product price is required.");
            RuleForEach(x => x.Price).SetValidator(new ProductPriceRequestValidator());
        }
    }
}
