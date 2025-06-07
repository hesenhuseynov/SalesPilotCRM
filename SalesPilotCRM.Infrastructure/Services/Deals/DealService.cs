using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Common.Constants.Messages;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal;
using SalesPilotCRM.Application.Features.Deals.Commands.UpdateDeal;
using SalesPilotCRM.Application.Features.Deals.Queries.Dtos;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Services;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Infrastructure.Services.Deals;

public class DealService : IDealService
{
    private readonly IAppWriteDbContext _context;
    private readonly IMapper _mapper;
    public DealService(IAppWriteDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Result<int>> CreateDealAsync(CreateDealDto dto, CancellationToken cancellationToken)
    {

        if (!await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId, cancellationToken))
            return Result<int>.Fail("Customer not found", ResultStatus.NotFound);

        if (!await _context.Users.AnyAsync(u => u.Id == dto.AssignedToUserId, cancellationToken))
            return Result<int>.Fail("Assigned user not found", ResultStatus.NotFound);

        if (!await _context.DealStages.AnyAsync(d => d.Id == dto.DealStageId, cancellationToken))
            return Result<int>.Fail("Deal stage not found", ResultStatus.NotFound);


        var entity = _mapper.Map<Deal>(dto);
        entity.CreatedAt = DateTime.UtcNow;

        await _context.Deals.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Ok(entity.Id, "Deal created successfully");
    }


    public async Task<Result> DeleteDealAsync(int id, byte[] rowVersion, CancellationToken cancellationToken)
    {
        var deal = await _context.Deals.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (deal is null)
            return Result.Fail(DealMessages.NotFound, ResultStatus.NotFound);

        _context.SetOriginalRowVersion(deal, rowVersion);

        try
        {
            deal.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(DealMessages.Deleted);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Fail(DealMessages.ConcurrencyConflict, ResultStatus.Conflict);
        }
    }




    //public Task<Result<DealDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Result<DealDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var deal = await _context.Deals
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (deal is null)
            return Result<DealDto>.Fail(DealMessages.NotFound, ResultStatus.NotFound);

        var dto = _mapper.Map<DealDto>(deal);
        return Result<DealDto>.Ok(dto, "Deal retrieved successfully.");
    }

    public async Task<Result> UpdateDealAsync(UpdateDealDto dto, CancellationToken cancellationToken)
    {
        var deal = await _context.Deals
            .FirstOrDefaultAsync(d => d.Id == dto.Id, cancellationToken);

        if (deal is null)
            return Result.Fail(DealMessages.NotFound, ResultStatus.NotFound);

        _context.SetOriginalRowVersion(deal, dto.RowVersion!);
        _mapper.Map(dto, deal);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(DealMessages.Updated);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Fail(DealMessages.ConcurrencyConflict, ResultStatus.Conflict);
        }
    }

}

