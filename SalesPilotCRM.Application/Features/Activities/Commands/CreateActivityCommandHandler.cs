using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Activities.Commands
{
    public class CreateActivityCommandHandler
         : IRequestHandler<CreateActivityCommand, Result<ActivityResponse>>
    {

        private readonly IActivityService _activityService;


        public CreateActivityCommandHandler(IActivityService activityService)
        {
            _activityService = activityService;
        }



        public async Task<Result<ActivityResponse>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var result = await _activityService.CreateAsync(request.ActivityDto, cancellationToken);

            if (!result.Success)
            {
                return Result<ActivityResponse>.Fail(result.Errors!, result.Status);

            }

            //return Result<ActivityResponse>.Ok(new ActivityResponse { Id = result.Data }, result.Message);
            return null;
        }
    }
}
