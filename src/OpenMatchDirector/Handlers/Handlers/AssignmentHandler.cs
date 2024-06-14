namespace OpenMatchDirector.Handlers.Handlers;


/*
public sealed class AssignmentHandler: IPipelineStep
{
    private List<string> ticketIds;
    
    public void Process(
        List<string> ticketIds,
        string host,
        int port,
        CancellationToken token)
    {
        var group = AssignmentHelper.NewAssignmentGroup(ticketIds, address, port);
        var assignRequest = new AssignmentHelper.RequestBuilder()
            .WithAssignmentGroup(group)
            .Build();

        var assignResponse = await beClient.AssignTicketsAsync(assignRequest, cancellationToken: token);
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