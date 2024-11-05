using FluentValidation;
using MarktGuru.Products.Application.Data.Products.Queries;

namespace MarktGuru.Products.Application.Validator.Products.Queries
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}
