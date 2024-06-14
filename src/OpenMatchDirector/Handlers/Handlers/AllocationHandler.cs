using Allocation;
using OpenMatchDirector.Clients.Agones;
using OpenMatchDirector.Utilities.Agones;

namespace OpenMatchDirector.Handlers.Handlers;


public class AllocationHandler(AllocationService.AllocationServiceClient agonesClient) 
    : IPipelineStep
{
    
    public void Process()
    {
        /*
        var request = new ServerAllocations.RequestBuilder()
            .WithMetadata()
            .WithNamespace()
            .WithGameSelectors()
            .Build();

        var response = agonesClient.AllocateAsync(request);
        response.ResponseAsync.Result.Address;
        */
        
        // Allocate
        var host = AgonesHelper.NewFakeHost();
        //Metrics - AddAllocationSuccess();
        
        
        // decide to continue or stop
    }
}