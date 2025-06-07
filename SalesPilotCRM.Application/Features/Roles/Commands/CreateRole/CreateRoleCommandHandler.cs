using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<CreateRoleResponse>>
    {
        private readonly IAppWriteDbContext _context;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IAppWriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Result<CreateRoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _context.Roles
                      .AnyAsync(r => r.Name == request.RoleDto.Name, cancellationToken);

                if (exists)
                {
                    return Result<CreateRoleResponse>.Fail("Role with this name already exists.");
                }
                var role = _mapper.Map<Role>(request.RoleDto);
                role.CreatedAt = DateTime.UtcNow;
                _context.Roles.Add(role);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<CreateRoleResponse>.Ok(
                 new CreateRoleResponse { RoleId = role.Id },
                 "Role successfully created."
             );
            }
            catch (Exception ex)
            {

                return Result<CreateRoleResponse>.Fail($"Unexpected error: {ex.Message}");
            }
        }
    }
}
