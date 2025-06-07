using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesPilotCRM.Application.Common.Constants.Messages;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Activities.Commands;
using SalesPilotCRM.Application.Features.Activity;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Services;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Infrastructure.Services;

public class ActivityService : IActivityService
{

    private readonly IAppReadDbContext _readDbContext;
    private readonly IAppWriteDbContext _writeDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<ActivityService> _logger;
    private readonly IMediator _mediator;
    public ActivityService(IAppReadDbContext readDbContext, IMapper mapper, ILogger<ActivityService> logger, IAppWriteDbContext writeDbContext,
        ICurrentUserService currentUserService, IMediator mediator)
    {
        _readDbContext = readDbContext;
        _mapper = mapper;
        _logger = logger;
        _writeDbContext = writeDbContext;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task<Result<int>> CreateAsync(CreateActivityDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Activity>(dto);
            _logger.LogInformation("Mapped Activity entity before saving: {@ActivityEntity}", entity);
            _logger.LogInformation("Mapped Activity IDs: ActivityTypeId={TypeId}, CustomerId={CustId}, CreatedById={CreatorId}",
                      entity.ActivityTypeId, entity.CustomerId, entity.CreatedById);

            await _writeDbContext.Activities.AddAsync(entity, cancellationToken);
            _logger.LogInformation("Activity added to context. State: {State}. Calling SaveChangesAsync...", _writeDbContext.Entry(entity).State);
            await _writeDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("📝 Activity saved successfully by UserId (from DTO): {UserId}, ActivityId: {ActivityId}", dto.CreateByUserId, entity.Id);
            return Result<int>.Ok(entity.Id, ActivityMessages.Created);

        }

        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "DbUpdateException saving Activity. Inner Exception: {InnerExceptionMessage}. DTO: {@ActivityDto}", dbEx.InnerException?.Message, dto);
            foreach (var entry in dbEx.Entries)
            {
                _logger.LogError("DbUpdateException Entry: Entity {EntityName}, State {State}. FKs: TypeId={TypeId}, CustId={CustId}, CreatedById={CreatorId}",
                    entry.Entity.GetType().Name, entry.State,
                    (entry.Entity as Activity)?.ActivityTypeId,
                    (entry.Entity as Activity)?.CustomerId,
                    (entry.Entity as Activity)?.CreatedById);
            }
            return Result<int>.Fail($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}", ResultStatus.InternalError);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected exception saving Activity. DTO: {@ActivityDto}", dto);
            return Result<int>.Fail($"Unexpected error: {ex.Message}", ResultStatus.InternalError);
        }

    }


    public async Task<Result<List<ActivityDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("📥 Getting all activities...");

        var activities = await _readDbContext.Activities
            .Include(a => a.ActivityType)
            .Include(a => a.Customer)
            .Include(a => a.CreatedBy)
            .ToListAsync(cancellationToken);


        if (!activities.Any())
        {
            _logger.LogWarning("⚠️ No activities found.");
            return Result<List<ActivityDto>>.Fail("No Activities found");
        }
        var dtoList = _mapper.Map<List<ActivityDto>>(activities);
        return Result<List<ActivityDto>>.Ok(dtoList, "Activities retrieved successfully.");
    }
}
