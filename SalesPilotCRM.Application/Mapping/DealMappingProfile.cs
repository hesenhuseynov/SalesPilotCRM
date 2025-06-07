using AutoMapper;
using SalesPilotCRM.Application.Features.Deals.Commands.CreateDeal;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Mapping
{
    public class DealMappingProfile : Profile
    {
        public DealMappingProfile()
        {
            CreateMap<CreateDealDto, Deal>();


        }
    }
}
