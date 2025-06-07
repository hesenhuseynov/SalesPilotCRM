using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers;

namespace SalesPilotCRM.Application.Services
{
    public interface IUserService
    {
        Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<List<UserDto>>> GetAllAsync(CancellationToken cancellationToken);


    }
}
