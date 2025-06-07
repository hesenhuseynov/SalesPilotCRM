using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.DeleteCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;

namespace SalesPilotCRM.Application.Services
{
    public interface ICustomerService
    {
        Task<Result<int>> CreateCustomerAsync(CreateCustomerDto dto, CancellationToken cancellationToken);

        Task<Result<int>> UpdateCustomerAsync(UpdateCustomerDto dto, CancellationToken cancellationToken);

        Task<Result<int>> DeleteCustomerAsync(DeleteCustomerDto dto, CancellationToken cancellationToken);

        Task<Result<List<CustomerDto>>> GetCustomersByStatusAsync(int statusId, CancellationToken cancellationToken);

        Task<Result<List<CustomerDto>>> GetAllCustomerList(CancellationToken cancellationToken);

    }
}