using Microsoft.AspNetCore.Mvc;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Application.Models.Validation;

namespace ZemingoCMS.API
{
    public abstract class APIController : ControllerBase
    {
        public static IActionResult GetCommandResponse(CommandResult result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            var errorType = result.Errors?.FirstOrDefault()?.Type;

            if (errorType == ValidationErrorType.InvalidProperty)
            {
                return new BadRequestObjectResult(result);
            }

            if (errorType == ValidationErrorType.NotFound)
            {
                return new NotFoundObjectResult(result);
            }

            return new ConflictObjectResult(result);
        }

        public static IActionResult GetCommandResponse<T>(CommandResult result) where T : class
        {
            if (result.IsSuccess)
            {
                var response = (ObjectCommandResult<T>)result;
                return new OkObjectResult(response.Result);
            }

            var errorType = result.Errors?.FirstOrDefault()?.Type;

            if (errorType == ValidationErrorType.InvalidProperty)
            {
                return new BadRequestObjectResult(result);
            }

            if (errorType == ValidationErrorType.NotFound)
            {
                return new NotFoundObjectResult(result);
            }

            return new ConflictObjectResult(result);
        }
    }
}
