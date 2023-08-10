using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using MediatR.Pipeline;

namespace MovieStoreApi.Behaviors;

[UsedImplicitly]
public class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest> 
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var results = await Task.WhenAll(_validators
            .Select(async v => await v.ValidateAsync(request, cancellationToken)));

        var failures = results
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
    }
}