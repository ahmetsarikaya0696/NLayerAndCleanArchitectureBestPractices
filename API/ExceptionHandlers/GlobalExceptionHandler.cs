using Application;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace API.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessages = new[] { exception.Message };

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(new { ErrorMessages = errorMessages }, cancellationToken);

            return true; // true => Başka bir middleware'e uğramayacak.
        }
    }
}
