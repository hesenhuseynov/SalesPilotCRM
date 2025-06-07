using AutoMapper;
using SalesPilotCRM.Application.Features.Users.Commands.CreateUser;
using SalesPilotCRM.Application.Features.Users.Queries.GetAllUsers;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>()
               .ForMember(dest => dest.RoleName, opti => opti.MapFrom(src => src.Role.Name));



        }
    }
}
