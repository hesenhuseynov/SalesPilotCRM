using Microsoft.EntityFrameworkCore;
using Moq;
using SalesPilotCRM.Application.EventHandlers.Customer;
using SalesPilotCRM.Application.Features.Activities.Commands;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Services;
using SalesPilotCRM.Domain.Events;
using SalesPilotCRM.Persistence.Contexts;

namespace SalesPilotCRM.UnitTests.Events
{
    public class CustomerEventTests
    {
        private readonly AppWriteDbContext _context;
        private readonly Mock<IActivityService> _mockActivityService;


        public CustomerEventTests()
        {
            var options = new DbContextOptionsBuilder<AppWriteDbContext>()
                 .UseInMemoryDatabase(databaseName: "TestDb")
                 .Options;

            _context = new AppWriteDbContext(options, new SystemCurrentUserService());
            _mockActivityService = new Mock<IActivityService>();
        }

        [Fact]
        public async Task Handle_CustomerCreatedEvent_ShouldCreateActivty()
        {
            await _context.ActivityTypes.AddAsync(new Domain.Entities.ActivityType { Id = 1, Name = "System event" });
            await _context.SaveChangesAsync();

            var handler = new CustomerCreatedEventHandler(_mockActivityService.Object);

            var @event = new CustomerCreatedEvent(
                CustomerId: 1,
                CreatedByUserId: -1,
                OccuredAt: DateTime.UtcNow
                );

            await handler.Handle(@event, CancellationToken.None);

            _mockActivityService.Verify(
               x => x.CreateAsync(
                   It.Is<CreateActivityDto>(dto =>
                       dto.Title == "Yeni Müşteri: 1" &&
                       dto.CustomerId == 1
                   ),
                   It.IsAny<CancellationToken>()
               ),

               Times.Once
           );


        }

        public class SystemCurrentUserService : ICurrentUserService
        {
            public string? UserId => "System";
            public string? Email => null;
            public bool IsAuthenticated => false;
        }

    }
}
