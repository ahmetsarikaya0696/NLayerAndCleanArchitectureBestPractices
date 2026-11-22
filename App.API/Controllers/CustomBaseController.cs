using App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        return result switch
        {
            ServiceResult<T>.Succeeded { StatusCode: HttpStatusCode.NoContent } => NoContent(),

            ServiceResult<T>.Succeeded { StatusCode: HttpStatusCode.Created } created =>
                Created(created.UrlAsCreated, new
                {
                    Data = created.Data,
                    ErrorMessages = (IReadOnlyList<string>?)null
                }),

            ServiceResult<T>.Succeeded success =>
                StatusCode((int)success.StatusCode, new
                {
                    Data = success.Data,
                    ErrorMessages = (IReadOnlyList<string>?)null
                }),

            ServiceResult<T>.Failed failure =>
                StatusCode((int)failure.StatusCode, new
                {
                    Data = default(T),
                    ErrorMessages = failure.ErrorMessages
                }),

            _ => StatusCode(500)
        };
    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        return result switch
        {
            ServiceResult.Succeeded { StatusCode: HttpStatusCode.NoContent } => NoContent(),

            ServiceResult.Succeeded success =>
                StatusCode((int)success.StatusCode, new
                {
                    ErrorMessages = (IReadOnlyList<string>?)null
                }),

            ServiceResult.Failed failure =>
                StatusCode((int)failure.StatusCode, new
                {
                    ErrorMessages = failure.ErrorMessages
                }),

            _ => StatusCode(500)
        };
    }
}
