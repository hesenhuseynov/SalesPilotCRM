using FluentValidation;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");


        }
    }
}
