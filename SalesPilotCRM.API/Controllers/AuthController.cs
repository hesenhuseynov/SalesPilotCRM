using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Helpers;
using SalesPilotCRM.Application.Features.Auth.Login;
using SalesPilotCRM.Application.Features.Auth.RefreshToken;
using SalesPilotCRM.Application.Features.Auth.Register;

namespace SalesPilotCRM.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;


        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPost]



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}
