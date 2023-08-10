using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Errors;

namespace MovieStoreApi.Extensions;

public static class ResponseExtension
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        var error = result.IsFailed ? result.Errors.FirstOrDefault() : null;
        return error != null ? new ObjectResult(error.Message){ StatusCode = ((HttpError)error).HttpStatusCode } : result.Value;
    }
    
    public static IActionResult ToActionResult(this Result result)
    {
        var error = result.IsFailed ? result.Errors.FirstOrDefault() : null;
        return error != null ? new ObjectResult(error.Message){ StatusCode = ((HttpError)error).HttpStatusCode } : new OkResult();
    }
    
    public static IActionResult ToCreatedResult(this Result result)
    {
        var error = result.IsFailed ? result.Errors.FirstOrDefault() : null;
        return error != null ? new ObjectResult(error.Message){ StatusCode = ((HttpError)error).HttpStatusCode } : new StatusCodeResult(StatusCodes.Status201Created);
    }
}