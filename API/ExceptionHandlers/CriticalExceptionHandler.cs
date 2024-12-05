using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace API.ExceptionHandlers
{
    public class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                Console.WriteLine("Hata ile ilgili SMS gönderildi");
            }

            return ValueTask.FromResult(false); // false => Bir sonraki handler'a gönder
        }
    }
}
