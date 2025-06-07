using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Helpers;
using SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.DeleteCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer;
using SalesPilotCRM.Application.Features.Customers.Queries.GetAll;
using SalesPilotCRM.Application.Features.Customers.Queries.GetByStatus;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;

namespace SalesPilotCRM.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        public readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            return result.ToActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("ByStatusId")]
        public async Task<IActionResult> GetCustomersByStatus([FromQuery] int statusId)
        {
            var result = await _mediator.Send(new GetCustomerListByStatusQuery(statusId));
            return result.ToActionResult();
        }
    }
}
