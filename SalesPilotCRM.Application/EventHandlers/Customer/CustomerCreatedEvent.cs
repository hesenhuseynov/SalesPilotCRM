using MediatR;
using Microsoft.Extensions.Logging;
using SalesPilotCRM.Application.Features.Activities.Commands;
using SalesPilotCRM.Application.Services;
using SalesPilotCRM.Domain.Events;

namespace SalesPilotCRM.Application.EventHandlers.Customer
{
    public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IActivityService _activityService;

        private readonly ILogger<CustomerCreatedEventHandler> _logger;

        private const int SystemEventActivityTypeId = 7;

        public CustomerCreatedEventHandler(IActivityService activityService, ILogger<CustomerCreatedEventHandler> logger)
        {
            _activityService = activityService;
            _logger = logger;
        }

        public async Task Handle(CustomerCreatedEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CustomerCreatedEvent for CustomerId: {CustomerId}", @event.CustomerId);

            var activityDto = new CreateActivityDto
            {
                Title = $"System Event",
                ActivityTypeId = SystemEventActivityTypeId, // System Event,
                Description = $"New Customer created: {@event.CustomerId}",
                CreateByUserId = @event.CreatedByUserId,
                CustomerId = @event.CustomerId,
                ActivityDate = DateTime.UtcNow
            };



            _logger.LogInformation("Creating activity: {@ActivityDto}", activityDto);

            var result = await _activityService.CreateAsync(activityDto, cancellationToken);

            if (!result.Success)
            {
                _logger.LogError("Failed to create activity for CustomerId: {CustomerId}. Errors: {Errors}", @event.CustomerId, result.Errors);
            }

            else
            {
                _logger.LogInformation("Activity created successfully for CustomerId: {CustomerId}. ActivityId: {ActivityId}", @event.CustomerId, result.Data);

            }
        }
    }
}
