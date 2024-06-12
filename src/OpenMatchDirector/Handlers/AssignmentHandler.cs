using OpenMatchDirector.Utilities.OpenMatch;

namespace OpenMatchDirector.Handlers;

/*
public sealed class AssignmentHandler: IChainHandler
{
    private IChainHandler Successor;
    
    public void Process()
    {
        var group = AssignmentHelper.NewAssignmentGroup(ticketIds, host.Address, host.Port);
        var assignRequest = new AssignmentHelper.RequestBuilder()
            .WithAssignmentGroup(group)
            .Build();

        var assignResponse =
            await beClient.AssignTicketsAsync(assignRequest, cancellationToken: stoppingToken);
        if (assignResponse.Failures.Count == 0)
        {
            LogSuccess();
            //Metrics - AddAssignmentSuccess();
        } 
        else 
        {
            LogError();
        }
    }

    private void LogSuccess()
    {
        
    }
    private void LogError()
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
*/