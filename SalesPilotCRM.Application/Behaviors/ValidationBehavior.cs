using FluentValidation;
using MediatR;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.Application.Behaviors
{


    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f is not null)
                    .ToList();

                if (failures.Any())
                {
                    var errors = failures.Select(f => f.ErrorMessage).ToList();
                    

                    var errorResponse = typeof(TResponse).Name.StartsWith("Result<")
                        ? CreateFailedResult<TResponse>(errors)
                        : throw new ValidationException(failures);

                    return errorResponse;
                }
            }

            return await next();
        }

        private static TResponse CreateFailedResult<TResponse>(List<string> errors)
        {
            var genericType = typeof(TResponse).GetGenericArguments().FirstOrDefault();
            var resultType = typeof(Result<>).MakeGenericType(genericType!);

            var failMethod = resultType.GetMethod(nameof(Result<object>.Fail), new[] { typeof(List<string>) })!;
            return (TResponse)failMethod.Invoke(null, new object[] { errors })!;
        }
    }

}
