using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Helpers;
using SalesPilotCRM.Application.Features.Activity.GetAll;

namespace SalesPilotCRM.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllActivitiesQuery());
            return result.ToActionResult();
        }


    }
}
