using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesPilotCRM.Application.Common.Constants.Messages;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Services;

namespace SalesPilotCRM.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IAppReadDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IAppReadDbContext context, IMapper mapper, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user is null)
            {
                _logger.LogWarning("User not found. ID: {UserId}", id);
                return Result<UserDto>.Fail("User not found", ResultStatus.NotFound);
            }

            var dto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Ok(dto, "User retrieved successfully.");
        }


        public async Task<Result<List<UserDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users
                  .Include(u => u.Role)
                .ToListAsync(cancellationToken);

            if (!users.Any())
                return Result<List<UserDto>>.Fail(UserMessages.NotFound, ResultStatus.NotFound);

            var dtoList = _mapper.Map<List<UserDto>>(users);
            return Result<List<UserDto>>.Ok(dtoList, UserMessages.Retrieved);
        }
    }
}
