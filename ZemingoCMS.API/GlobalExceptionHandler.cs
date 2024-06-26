using Microsoft.AspNetCore.Diagnostics;

namespace ZemingoCMS.API
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            httpContext.Response.StatusCode = 500;

            await httpContext.Response.WriteAsJsonAsync(
                new { Error = "Internal server error please try again later" },
                cancellationToken);

            return true;
        }
    }
}
