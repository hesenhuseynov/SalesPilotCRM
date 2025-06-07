using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Common.Constants.Messages;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces;

namespace SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdResponse>>
    {
        public readonly IAppReadDbContext _context;


        public GetCustomerByIdQueryHandler(IAppReadDbContext context)
        {
            _context = context;
        }


        public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerStatus)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (customer == null)
            {
                return Result<GetCustomerByIdResponse>.Fail(
                    CustomerMessages.NotFound,
                    ResultStatus.NotFound);
            }

            var dto = new GetCustomerByIdResponse
            {
                Id = customer.Id,
                FullName = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email,
                Phone = customer.Phone,
                CompanyName = customer.CompanyName,
                Status = customer.CustomerStatus.Name,
                RowVersion = customer.RowVersion
            };

            return Result<GetCustomerByIdResponse>.Ok(dto);

        }
    }
}
