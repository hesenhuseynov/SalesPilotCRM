using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesPilotCRM.Application.Common.Constants.Messages;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Customers.Commands.CreateCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.DeleteCustomer;
using SalesPilotCRM.Application.Features.Customers.Commands.Update_Customer;
using SalesPilotCRM.Application.Features.Customers.Queries.GetCustomerById;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Services;
using SalesPilotCRM.Domain.Entities;
using SalesPilotCRM.Infrastructure.Services.Deals;
using Serilog.Context;

namespace SalesPilotCRM.Infrastructure.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppWriteDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DealService> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CustomerService(IAppWriteDbContext context, IMapper mapper, ILogger<DealService> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<Result<int>> CreateCustomerAsync(CreateCustomerDto dto, CancellationToken cancellationToken)
        {


            if (dto.AssignedToUserId.HasValue)
            {
                if (dto.AssignedToUserId == 0)
                {
                    _logger.LogWarning("AssignedToUserId cannot be 0.");
                    return Result<int>.Fail("AssignedToUserId cannot be 0.", ResultStatus.BadRequest);
                }

                var userExists = await _context.Users.AnyAsync(c => c.Id == dto.AssignedToUserId, cancellationToken);

                if (!userExists)
                {
                    _logger.LogWarning("Invalid AssignedToUserId.");

                    return Result<int>.Fail("invalid  AssignedToUserId", ResultStatus.BadRequest);

                }
            }
            using (LogContext.PushProperty("UserId", _currentUserService.UserId ?? "System"))
            {

                _logger.LogInformation("📌 Create customer request received for: {Email}", dto.Email);


                var exists = await _context.Customers.AnyAsync(c => c.Email == dto.Email, cancellationToken);

                if (exists)
                {
                    _logger.LogWarning("⚠️ Customer with this email already exists: {Email}", dto.Email);
                    return Result<int>.Fail(CustomerMessages.AlreadyExists, ResultStatus.Conflict);
                }

                var entity = _mapper.Map<Customer>(dto);

                try
                {
                    await _context.Customers.AddAsync(entity, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }


                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Error while saving customer to database.");
                    return Result<int>.Fail("Error while saving customer.", ResultStatus.InternalError);
                }

                entity.MarkAsCreated();
                await _context.SaveChangesAsync(cancellationToken);
                //var events = entity.DomainEvents;
                foreach (var domainEvent in entity.DomainEvents)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }

                entity.ClearDomainEvents();
                _logger.LogInformation("✅ Customer created with ID: {CustomerId}", entity.Id);
                return Result<int>.Ok(entity.Id, CustomerMessages.Created);

            }
        }



        public async Task<Result<int>> UpdateCustomerAsync(UpdateCustomerDto dto, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("UserId", _currentUserService.UserId ?? "System"))
            using (LogContext.PushProperty("CustomerId", dto.Id))
            {
                _logger.LogInformation("🔄 Update customer request started.");

                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);

                if (customer is null)
                {
                    _logger.LogWarning("⚠️ Customer not found. ID: {CustomerId}", dto.Id);
                    return Result<int>.Fail(CustomerMessages.NotFound, ResultStatus.NotFound);
                }


                //_context.Entry(customer).OriginalValues["RowVersion"] = dto.RowVersion!;
                _context.SetOriginalRowVersion(customer, dto.RowVersion!);

                _mapper.Map(dto, customer);

                Console.WriteLine("DTO RowVersion: " + Convert.ToBase64String(dto.RowVersion!));
                Console.WriteLine("Entity RowVersion: " + Convert.ToBase64String(customer.RowVersion));

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("✅ Customer updated successfully. ID: {CustomerId}", customer.Id);
                    return Result<int>.Ok(customer.Id, CustomerMessages.Updated);
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogWarning("⚠️ Concurrency conflict detected while updating customer. ID: {CustomerId}", dto.Id);
                    return Result<int>.Fail(CustomerMessages.ConcurrencyConflict, ResultStatus.Conflict);
                }
            }
        }
        public async Task<Result<int>> DeleteCustomerAsync(DeleteCustomerDto dto, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("UserId", _currentUserService.UserId ?? "System"))
            using (LogContext.PushProperty("CustomerId", dto.Id))
            {

                _logger.LogInformation("🗑️ Delete customer request started.");

                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == dto.Id);

                if (customer is null)
                {
                    _logger.LogWarning("Customer not found. ID: {CustomerId}", dto.Id);
                    return Result<int>.Fail(CustomerMessages.NotFound, ResultStatus.NotFound);

                }
                try
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("✅ Customer deleted successfully. ID: {CustomerId}", customer.Id);
                    return Result<int>.Ok(customer.Id, CustomerMessages.Deleted);

                }
                catch (DbUpdateConcurrencyException)
                {

                    _logger.LogWarning("⚠️ Concurrency conflict detected while deleting customer. ID: {CustomerId}", dto.Id);
                    return Result<int>.Fail(CustomerMessages.ConcurrencyConflict, ResultStatus.Conflict);
                }
            }
        }


        public async Task<Result<List<CustomerDto>>> GetCustomersByStatusAsync(int statusId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("📥 Getting customers by status. StatusId: {StatusId}", statusId);

            var customers = await _context.Customers
                .Include(c => c.CustomerStatus)
                .Where(c => c.CustomerStatusId == statusId && c.IsActive)
                .ToListAsync(cancellationToken);

            if (!customers.Any())
            {
                _logger.LogWarning("⚠️ No customers found for status ID: {StatusId}", statusId);
                return Result<List<CustomerDto>>.Fail("No customers found.");
            }
            var dtoList = _mapper.Map<List<CustomerDto>>(customers);
            return Result<List<CustomerDto>>.Ok(dtoList, "Customers retrieved successfully.");
        }

        public async Task<Result<List<CustomerDto>>> GetAllCustomerList(CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ToListAsync();

            var dtoList = _mapper.Map<List<CustomerDto>>(customers);

            if (!customers.Any())
            {
                return Result<List<CustomerDto>>.Fail("no customer Found");
            }

            return Result<List<CustomerDto>>.Ok(dtoList, "Customers retrieved succeffuly");

        }
    }
}
