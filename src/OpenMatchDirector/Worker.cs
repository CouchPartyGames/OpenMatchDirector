using Grpc.Core;
using OpenMatchDirector.OpenMatch;

namespace OpenMatchDirector;

public class Worker(ILogger<Worker> logger,
        //IProfileFunction profiles,
        BackendService.BackendServiceClient beClient)
    : BackgroundService, IHostedLifecycleService
{
    //private readonly IProfileFunction _profileFuncs;
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var profiles = new Profiles();
        var profileFuncs = profiles.GenerateProfiles();
            
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var myItem in profileFuncs)
            {
                
                var request = new FetchMatches.RequestBuilder()
                    .WithFunctionConfig(myItem.Func)
                    .WithMatchProfile(myItem.Profile)
                    .Build();
                
                    // Fetch Matches
                using var call = beClient.FetchMatches(request);
                await foreach (var response in call.ResponseStream.ReadAllAsync(stoppingToken)) {
                    //_logger.LogInformation(response);
                    
                    // Allocate
                    // Assignment
                }
            }

            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
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