namespace OpenMatchDirector.Handlers;

/*
public sealed class MatchHandler : IChainHandler
{
    
    public void Process()
    {
        
        using var call = beClient.FetchMatches(request, cancellationToken: stoppingToken);
        await foreach (var response in call.ResponseStream.ReadAllAsync(stoppingToken))
        {
            var ticketList = response.Match.Tickets.Select(x => x.Id).ToList();
            if (ticketList.Count == 0)
                break;
        }
    }
}
*/