using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.CustomerStatuses.Queries;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Infrastructure.Services.CustomerStatus
{
    public class CustomerStatusService : ICustomerStatusService
    {
        private readonly IAppReadDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerStatusService> _logger;

        public CustomerStatusService(IAppReadDbContext context, IMapper mapper, ILogger<CustomerStatusService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CustomerStatusDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("📥 Retrieving CustomerStatus by ID: {Id}", id);

            var status = await _context.CustomerStatuses
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (status is null)
            {
                _logger.LogWarning("⚠️ CustomerStatus not found. ID: {Id}", id);
                return Result<CustomerStatusDto>.Fail("CustomerStatus not found.", ResultStatus.NotFound);
            }


            var dto = _mapper.Map<CustomerStatusDto>(status);
            return Result<CustomerStatusDto>.Ok(dto, "Status retrieved successfully.");

        }



        public async Task<Result<List<CustomerStatusDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("📥 Getting all customer statuses...");

            var statuses = await _context.CustomerStatuses
                .Where(s => s.IsActive)
                .OrderBy(s => s.Id)
                .ToListAsync(cancellationToken);

            if (!statuses.Any())
            {
                _logger.LogWarning("⚠️ No customer statuses found.");
                return Result<List<CustomerStatusDto>>.Fail("No customer statuses found.");
            }

            var dtoList = _mapper.Map<List<CustomerStatusDto>>(statuses);
            return Result<List<CustomerStatusDto>>.Ok(dtoList, "Statuses retrieved successfully.");
        }
    }
}
