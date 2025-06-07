using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Activities.Commands;
using SalesPilotCRM.Application.Features.Activity;

namespace SalesPilotCRM.Application.Services
{
    public interface IActivityService
    {
        Task<Result<List<ActivityDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<int>> CreateAsync(CreateActivityDto dto, CancellationToken cancellationToken);

    }
}
