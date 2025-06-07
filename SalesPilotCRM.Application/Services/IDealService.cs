using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal;
using SalesPilotCRM.Application.Features.Deals.Commands.UpdateDeal;
using SalesPilotCRM.Application.Features.Deals.Queries.Dtos;

namespace SalesPilotCRM.Application.Services
{
    public interface IDealService
    {
        Task<Result<int>> CreateDealAsync(CreateDealDto dto, CancellationToken cancellationToken);
        Task<Result> UpdateDealAsync(UpdateDealDto dto, CancellationToken cancellationToken);
        Task<Result> DeleteDealAsync(int id, byte[] rowversion, CancellationToken cancellationToken);
        Task<Result<DealDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        //Task<Result<List<DealDto>>> GetListAsync(CancellationToken cancellationToken);
    }
}
