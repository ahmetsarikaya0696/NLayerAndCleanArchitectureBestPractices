using Application;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace API.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var serviceResult = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);

            httpContext.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(serviceResult, cancellationToken);

            return true; // true => Başka bir middleware'e uğramayacak.
        }
    }
}
