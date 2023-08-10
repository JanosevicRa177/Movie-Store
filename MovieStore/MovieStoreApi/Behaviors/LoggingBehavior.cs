using System.Diagnostics;
using System.Text.Json;
using MediatR;

namespace MovieStoreApi.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();
        var requestNameWithGuid = $"{requestName} [{requestGuid}]";
        
        TResponse response;

        _logger.LogInformation("[START] {RequestNameWithGuid}", requestNameWithGuid);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("[PROPS] {RequestNameWithGuid} {Serialize}", requestNameWithGuid,
                JsonSerializer.Serialize(request));
            response = await next();
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("[END] {RequestNameWithGuid}; Execution time={StopwatchElapsedMilliseconds}ms",
                requestNameWithGuid, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}