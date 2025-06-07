using Microsoft.AspNetCore.Mvc;
using SalesPilotCRM.API.Models;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;

namespace SalesPilotCRM.API.Helpers
{
    public static class ResultExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this Result<T> result)
        {
            return result.Success
                ? ApiResponse<T>.Ok(result.Data!, result.Message)
                : ApiResponse<T>.Fail(result.Errors!, result.Message);
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            var apiResponse = result.ToApiResponse();
            return result.Status switch
            {
                ResultStatus.Success => new OkObjectResult(apiResponse),
                ResultStatus.ValidationError => new BadRequestObjectResult(apiResponse),
                ResultStatus.BadRequest => new BadRequestObjectResult(apiResponse),
                ResultStatus.NotFound => new NotFoundObjectResult(apiResponse),
                ResultStatus.Unauthorized => new UnauthorizedObjectResult(apiResponse),
                ResultStatus.Forbidden => new ObjectResult(apiResponse) { StatusCode = 403 },
                ResultStatus.Conflict => new ConflictObjectResult(apiResponse),
                ResultStatus.InternalError => new ObjectResult(apiResponse) { StatusCode = 500 },
                _ => new ObjectResult(apiResponse) { StatusCode = 500 }
            };
        }
    }
}
