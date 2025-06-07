using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<Result<RegisterResponse>>
    {
        public RegisterRequest RegisterRequest { get; set; } = null!;
    }

}
