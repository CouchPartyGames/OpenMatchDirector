namespace OpenMatchDirector.Clients.OpenMatchBackend;

public sealed class BackendAssignTickets
{
    public async Task<bool> AssignTickets(BackendService.BackendServiceClient client, AssignTicketsRequest request)
    {
        var response  =  await client.AssignTicketsAsync(request);
        return true;
    }
}