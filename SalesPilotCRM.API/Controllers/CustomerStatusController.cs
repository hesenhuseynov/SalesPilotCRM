using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Helpers;
using SalesPilotCRM.Application.Features.CustomerStatuses.Queries.GetById;

namespace SalesPilotCRM.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CustomerStatusController : ControllerBase
    {
        private readonly IMediator _mediator;


        public CustomerStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCustomerStatusByIdQuery(id));

            return result.ToActionResult();
        }
    }
}
