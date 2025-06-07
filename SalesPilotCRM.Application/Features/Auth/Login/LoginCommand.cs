using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Auth.Login
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public LoginRequest LoginRequest { get; set; } = null!;
    }



}
