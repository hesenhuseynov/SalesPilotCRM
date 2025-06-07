using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Activities.Commands
{
    public record CreateActivityCommand(CreateActivityDto ActivityDto)
        : IRequest<Result<ActivityResponse>>;



    public class ActivityResponse
    {
        public int Id { get; set; }
    }
}
