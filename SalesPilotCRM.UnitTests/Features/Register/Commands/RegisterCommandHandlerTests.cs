using Moq;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Auth.Register;
using SalesPilotCRM.Application.Interfaces.Auth;

namespace SalesPilotCRM.UnitTests.Features.Register.Commands
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly RegisterCommandHandler _handler;

        public RegisterCommandHandlerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _handler = new RegisterCommandHandler(_mockAuthService.Object);
        }

        [Fact]
        public async Task Should_ReturnSuccess_When_RegistrationSucceeds()

        {

            var request = new RegisterCommand
            {
                RegisterRequest = new RegisterRequest
                {
                    Email = "newuser@example.com",
                    Password = "Secure123!",
                    FullName = "Test User"
                }
            };

            var expectedResponse = Result<RegisterResponse>.Ok(new RegisterResponse
            {
                AccessToken = "mocked_token"
            }, "User created successfully");


            _mockAuthService
              .Setup(x => x.RegisterAsync(request.RegisterRequest, It.IsAny<CancellationToken>()))
              .ReturnsAsync(expectedResponse);

            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.True(result.Success);
            Assert.Equal("User created successfully", result.Message);
            Assert.Equal("mocked_token", result.Data.AccessToken);
        }


        [Fact]
        public async Task Should_ReturnFail_When_ServiceReturnsError()
        {
            var request = new RegisterCommand
            {
                RegisterRequest = new RegisterRequest
                {
                    Email = "existing@example.com",
                    Password = "Secure123!",
                    FullName = "Existing User"
                }
            };



            var failResponse = Result<RegisterResponse>.Fail("mail already in use");

            _mockAuthService
                            .Setup(x => x.RegisterAsync(request.RegisterRequest, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(failResponse);


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("mail already in use", result.Message);

        }
    }
}
