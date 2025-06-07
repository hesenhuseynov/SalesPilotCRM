using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
    {
        private readonly IAppWriteDbContext _context;
        private readonly IMapper _mapper;


        public CreateUserCommandHandler(IAppWriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _context.Users.AnyAsync(u => u.Email == request.UserDto.Email, cancellationToken);
                if (exists)
                    return Result<CreateUserResponse>.Fail("User with this email already exists.");

                var user = _mapper.Map<User>(request.UserDto);
                user.CreatedAt = DateTime.UtcNow;

                _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<CreateUserResponse>.Ok(
                new CreateUserResponse { UserId = user.Id },
                "User successfully created."
            );

            }
            catch (Exception ex)
            {

                return Result<CreateUserResponse>.Fail($"Unexpected error: {ex.Message}");
            }

        }
    }
}
