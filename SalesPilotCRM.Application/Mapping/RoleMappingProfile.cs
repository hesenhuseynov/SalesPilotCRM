using AutoMapper;
using SalesPilotCRM.Application.Features.Roles.Commands.CreateRole;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<CreateRoleDto, Role>();
        }
    }
}
