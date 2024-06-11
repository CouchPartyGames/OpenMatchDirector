using Grpc.Core;
using OpenMatchDirector.Utilities.Agones;
using OpenMatchDirector.Utilities.OpenMatch;
using OpenMatchDirector.Utilities.Profiles;

namespace OpenMatchDirector;

public class Worker(ILogger<Worker> logger,
        IProfileFunctionMap profiles,
        BackendService.BackendServiceClient beClient)
    : BackgroundService, IHostedLifecycleService
{
    //private readonly IProfileFunctionMap _map = profiles;
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var profileFuncs = profiles.GenerateProfiles();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var map in profileFuncs)
            {
                
                var request = new MatchHelper.RequestBuilder()
                    .WithFunctionConfig(map.Function)
                    .WithMatchProfile(map.Profile)
                    .Build();
                
                _logger.LogInformation("Request: {request}", request);

                try
                {
                    // Fetch Matches
                    using var call = beClient.FetchMatches(request, cancellationToken: stoppingToken);
                    await foreach (var response in call.ResponseStream.ReadAllAsync(stoppingToken))
                    {
                        var ticketList = response.Match.Tickets.Select(x => x.Id).ToList();
                        RepeatedField<string> ticketIds = [..ticketList];

                        // Allocate
                        var host = AgonesHelper.NewFakeHost();
                        //Metrics - AddAllocationSuccess();

                        var group = AssignmentHelper.NewAssignmentGroup(ticketIds, host.Address, host.Port);
                        var assignRequest = new AssignmentHelper.RequestBuilder()
                            .WithAssignmentGroup(group)
                            .Build();

                        var assignResponse =
                            await beClient.AssignTicketsAsync(assignRequest, cancellationToken: stoppingToken);
                        if (assignResponse.Failures.Count == 0)
                        {
                            //Metrics - AddAssignmentSuccess();
                        } 
                        else 
                        {
                            assignResponse.Failures.ToList().ForEach(x =>
                            {
                                logger.LogError("Assigment Error: {TicketId} Reason: {Reason} ", 
                                    x.TicketId, x.Cause.ToString());
                                //Metrics.AddAssignmentFailureNotFound();
                                //Metrics.AddAssignmentFailureUnknown();
                            });
                        }
                    }
                }
                catch (RpcException ex)
                {
                    _logger.LogError("Error: {Message}", ex.Message);
                }
            }

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
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