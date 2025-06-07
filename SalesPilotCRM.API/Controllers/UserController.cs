using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Helpers;
using SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers;
using SalesPilotCRM.Application.Features.Users.Queries.GetUserByIdQuery;

namespace SalesPilotCRM.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQuery query)
        {
            var result = await _mediator.Send(query);

            return result.ToActionResult();
        }



        [HttpGet("GetAllUser")]

        public async Task<IActionResult> GetUserList()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return result.ToActionResult();
        }



    }
}
