using MediatR;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces.Auth;

namespace SalesPilotCRM.Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {

        private readonly IAuthService _authService;


        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;

        }


        public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            return await _authService.RegisterAsync(request.RegisterRequest, cancellationToken);

        }
    }
}
