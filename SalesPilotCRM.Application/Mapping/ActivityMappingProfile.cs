using AutoMapper;
using SalesPilotCRM.Application.Features.Activities.Commands;
using SalesPilotCRM.Application.Features.Activity;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Mapping
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {

            CreateMap<Activity, ActivityDto>()
                .ForMember(dest => dest.ActivityTypeName, opt => opt.MapFrom(src => src.ActivityType.Name))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FirstName + " " + src.Customer.LastName : null));
            //.ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.FullName));

            CreateMap<CreateActivityDto, Activity>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.CreatedByUser, opt => opt.Ignore())
                  .ForMember(dest => dest.ActivityType, opt => opt.Ignore())
                  .ForMember(dest => dest.Customer, opt => opt.Ignore())
                  .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreateByUserId));
        }
    }
}