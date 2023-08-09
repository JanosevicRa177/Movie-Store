using Microsoft.Extensions.Options;
using MovieStoreApi.Service.Email;

namespace MovieStoreApi.Service;

public class HostedService :  BackgroundService
{
    private readonly ILogger _logger;
    private readonly TimeOptions _timeOptions;
    private readonly IServiceProvider _serviceProvider;

    public HostedService(ILogger<HostedService> logger, TimeOptions timeOptions, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _timeOptions = timeOptions;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[START] Hosted service running");
        DoWork();

        using PeriodicTimer timer = new(TimeSpan.FromMinutes(_timeOptions.Minutes));

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken)) DoWork();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("[END] Timed Hosted Service is stopping");
        }
    }
    
    private void DoWork()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            emailService.SendExpirationEmails();
        }
        _logger.LogInformation("[PROPS] Sending emails");
    }
}