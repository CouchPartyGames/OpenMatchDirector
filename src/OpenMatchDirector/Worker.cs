using OpenMatchDirector.OpenMatch;

namespace OpenMatchDirector;

public class Worker : BackgroundService, IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;

    private readonly BackendService.BackendServiceClient _beClient;
    private readonly QueryService.QueryServiceClient _queryClient;

    public Worker(ILogger<Worker> logger,
        BackendService.BackendServiceClient beClient,
        QueryService.QueryServiceClient queryClient)
    {
        _logger = logger;
        _beClient = beClient;
        _queryClient = queryClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            
                // Allocate Dedicated Server
                // Assign Dedicated Server
            await Task.Delay(1000, stoppingToken);
        }
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopped at: {time}", DateTimeOffset.Now);
        return Task.CompletedTask;
    }
}