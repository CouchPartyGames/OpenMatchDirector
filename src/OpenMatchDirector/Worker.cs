using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
                
                var request = new FetchMatches.RequestBuilder()
                    .WithFunctionConfig(map.Function)
                    .WithMatchProfile(map.Profile)
                    .Build();
                
                _logger.LogInformation("Request: {request}", request);
                
                    // Fetch Matches
                using var call = beClient.FetchMatches(request);
                await foreach (var response in call.ResponseStream.ReadAllAsync(stoppingToken))
                {

                    RepeatedField<string> ticketIds = [];
                    foreach (var id in response.Match.Tickets)
                    {
                       ticketIds.Add(id.Id); 
                    }
                    
                        // Allocate
                    var connection = "192.168.1.1:5000";
                    
                        // Assignment
                    AssignmentGroup assignGroup = new AssignmentGroup
                    {
                        Assignment =
                        {
                            Connection = connection
                        }
                    };
                    //test.Assignment.Extensions = new MapField<string, Any>();
                    assignGroup.TicketIds.Add(ticketIds);
                    
                    var assignRequest = new Assign.RequestBuilder()
                        .WithAssignmentGroup(assignGroup)
                        .Build();
                    var assignResponse = await beClient.AssignTicketsAsync(assignRequest);
                    if (assignResponse.Failures.Count > 0)
                    {
                        
                    }
                }
            }

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("hello");
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