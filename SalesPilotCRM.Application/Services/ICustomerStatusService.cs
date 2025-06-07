using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.CustomerStatuses.Queries;

namespace SalesPilotCRM.Application.Services
{
    public interface ICustomerStatusService
    {
        Task<Result<CustomerStatusDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<List<CustomerStatusDto>>> GetAllAsync(CancellationToken cancellationToken);



    }
}
