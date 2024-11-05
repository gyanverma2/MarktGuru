using FluentValidation;
using MarktGuru.Products.Application.Handlers.Products.Queries;

namespace MarktGuru.Products.Application.Validator.Products.Queries
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
