using Moq;
using SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer;
using SalesPilotCRM.Application.Interfaces;

namespace SalesPilotCRM.UnitTests.Features.Customers.Commands
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<IAppWriteDbContext> _mockDbContext;
        private readonly CreateCustomerCommandHandler _handler;


        public CreateCustomerCommandHandlerTests()
        {
            _mockDbContext = new Mock<IAppWriteDbContext>();


            //_handler = new CreateCustomerCommandHandler(_mockDbContext.Object, mapper: null);
        }


        //[Fact]
        //public async Task Handle_Should_Return_Conflict_When_Email_Already_Exists()
        //{
        //    var command = new CreateCustomerCommand
        //    {
        //        CustomerDto = new CreateCustomerDto
        //        {
        //            Email = "exist@example.com",
        //            FirstName = "Test"
        //        }
        //    };


         

       

    }
}
