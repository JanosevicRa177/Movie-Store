using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieStoreApi.Extensions;

namespace MovieStoreApi.Filters;

public class ValidationExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ValidationExceptionFilter> _logger;
    
    public ValidationExceptionFilter(ILogger<ValidationExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        
        if (context.Exception is not ValidationException validationException) return;
        _logger.LogError("[ERROR] Validation exception occurred while executing request");
        context.Result = context.HttpContext.InvalidResponseFrom(validationException);
        return;
    }
}