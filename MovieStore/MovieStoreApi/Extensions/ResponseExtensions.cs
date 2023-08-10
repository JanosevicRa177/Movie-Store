using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApi.Errors;

namespace MovieStoreApi.Extensions;

public static class ResponseExtension
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        var error = result.IsFailed ? result.Errors.FirstOrDefault() : null;
        return error != null ? InvalidResponseFrom(((HttpError)error).HttpStatusCode,error.Message) : result.Value;
    }
    
    public static IActionResult ToActionResult(this Result result)
    {
        var error = result.IsFailed ? result.Errors.FirstOrDefault() : null;
        return error != null ? InvalidResponseFrom(((HttpError)error).HttpStatusCode,error.Message) : new OkResult();
    }
    
    public static IActionResult ToCreatedResult(this Result result)
    {
        var error = result.IsFailed ? result.Errors.FirstOrDefault() : null;
        return error != null ? InvalidResponseFrom(((HttpError)error).HttpStatusCode,error.Message) : new StatusCodeResult(StatusCodes.Status201Created);
    }

    private static ObjectResult InvalidResponseFrom(int status, string message)
    {
        var problemDetails = ValidationProblemDetails(status);
        problemDetails.Errors["error"] = new[]{message};
        return ToResult(problemDetails);
    }
    private static ObjectResult ToResult(ValidationProblemDetails problemDetails) =>
        new(problemDetails)
        {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
        };
    private static ValidationProblemDetails ValidationProblemDetails(int code) =>
        new()
        {
            Status = code,
            Detail = "Please refer to the errors property for additional details."
        };
}