using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Helpers;
using SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal;

namespace SalesPilotCRM.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class DealController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DealController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDealCommand command)
        {
            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }


    }
}
