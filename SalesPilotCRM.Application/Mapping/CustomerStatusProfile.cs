using AutoMapper;
using SalesPilotCRM.Application.Features.CustomerStatuses.Queries;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Mapping
{
    public class CustomerStatusProfile : Profile
    {
        public CustomerStatusProfile()
        {
            CreateMap<CustomerStatus, CustomerStatusDto>();
        }
    }
}
