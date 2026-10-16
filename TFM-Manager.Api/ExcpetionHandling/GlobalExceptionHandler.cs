using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TFM_Manager.Shared.Common;

namespace TFM_Manager.Api.ExcpetionHandling
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
           exception,
           "Nem kezelt kivétel. Útvonal: {Path}",
           httpContext.Request.Path);

            var response = AppResponse<object>.Failure(
    ErrorCodes.General.UnexpectedError,
    "Váratlan hiba történt a kérés feldolgozása során.");

            httpContext.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                response,
                cancellationToken);

            return true;
        }
    }
}
