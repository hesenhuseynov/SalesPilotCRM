using AutoMapper;
using SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Application.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>()
        .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
            srcMember != null && !(srcMember is byte[] bytes && bytes.Length == 0)));


            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName}  {src.LastName}"))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.CustomerStatus.Name));


           
        }
    }
}
