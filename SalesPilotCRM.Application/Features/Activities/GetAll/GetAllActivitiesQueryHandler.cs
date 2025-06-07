using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Application.Features.Activity.GetAll
{

    public record GetAllActivitiesQuery : IRequest<Result<List<ActivityDto>>>;

    public class GetAllActivitiesQueryHandler
         : IRequestHandler<GetAllActivitiesQuery, Result<List<ActivityDto>>>
    {
        private readonly IActivityService _activityService;

        public GetAllActivitiesQueryHandler(IActivityService activityService)
        {
            _activityService = activityService;
        }



        public async Task<Result<List<ActivityDto>>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            return await _activityService.GetAllAsync(cancellationToken);

        }
    }
}
