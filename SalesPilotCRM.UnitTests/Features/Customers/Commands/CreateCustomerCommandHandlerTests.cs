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

        //}

        //[Fact]
        //public async Task Handle_Should_Throw_Concurrency_When_RowVersion_Mismatch()
        //{
        //    // 🧪 Arrange – əvvəlki customer
        //    var existingCustomer = new Customer
        //    {
        //        Id = 1,
        //        FirstName = "Test",
        //        RowVersion = Convert.FromBase64String("AAAAAAABBBE=")
        //    };

        //    // 🧪 Yeni gələn DTO – fərqli RowVersion göndərilir
        //    var updatedDto = new UpdateCustomerDto
        //    {
        //        Id = 1,
        //        FirstName = "Updated",
        //        RowVersion = Convert.FromBase64String("AAAAAAAXXXX=") // intentional conflict
        //    };

        //    // 📦 MockQueryable.Moq ilə DB set mock-u
        //    var mockSet = new List<Customer> { existingCustomer }
        //        .AsQueryable()
        //        .BuildMockDbSet();

        //    // 🧱 Mock DbContext
        //    var mockContext = new Mock<IAppWriteDbContext>();
        //    mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

        //    // ❗ Burada SaveChangesAsync çağırılınca Concurrency exception atsın
        //    mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //               .ThrowsAsync(new DbUpdateConcurrencyException());

        //    // 🧠 Mapper lazımdır deyə boş mock veririk
        //    var mockMapper = new Mock<IMapper>();

        //    // 🎯 Service yarat
        //    var service = new CustomerService(mockContext.Object, mockMapper.Object);

        //    // 🧨 Act
        //    var result = await service.UpdateCustomerAsync(updatedDto, CancellationToken.None);

        //    // ✅ Assert – uğursuzluq olmalıdır (conflict)
        //    result.Success.Should().BeFalse();
        //    result.Status.Should().Be(ResultStatus.Conflict);
        //    result.Message.Should().Be(CustomerMessages.ConcurrencyConflict);
        //}




    }
}
