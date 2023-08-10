using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MovieStoreApi.Extensions;

public static class ObjectResultExtensions
{
    public static BadRequestObjectResult InvalidResponseFrom(this HttpContext context,
        ValidationException validationException)
    {
        if (validationException == null)
        {
            throw new ArgumentNullException(nameof(validationException));
        }

        var problemDetails = ValidationProblemDetails(context);
        CopyErrorsFrom(problemDetails, validationException.Errors);
        return ToBadRequestResult(problemDetails);
    }
    private static BadRequestObjectResult ToBadRequestResult(ValidationProblemDetails problemDetails) =>
        new(problemDetails)
        {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
        };
    private static ValidationProblemDetails ValidationProblemDetails(HttpContext context) =>
        new()
        {
            Instance = context.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };
    private static void CopyErrorsFrom(ValidationProblemDetails problemDetails, IEnumerable<ValidationFailure> validationErrors)
    {
        foreach (var validationError in validationErrors)
        {
            var key = validationError.PropertyName;
            if (!problemDetails.Errors.TryGetValue(key, out var messages))
            {
                messages = Array.Empty<string>();
            }
            messages = messages.Concat(new[] { validationError.ErrorMessage }).ToArray();
            problemDetails.Errors[key] = messages;
        }
    }
}