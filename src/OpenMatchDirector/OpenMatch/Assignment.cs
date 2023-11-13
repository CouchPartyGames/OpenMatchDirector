namespace OpenMatchDirector.OpenMatch;

public class Assign
{
    public async Task<bool> AssignTickets(BackendService.BackendServiceClient client, AssignTicketsRequest request)
    {
        var response  = client.AssignTicketsAsync(request);
        return true;
    }
    
    public class RequestBuilder
    {
        private AssignTicketsRequest _request = new();
        
        public RequestBuilder WithAssignmentGroup(AssignmentGroup group)
        {
            _request.Assignments.Add(group);
            return this;
        }

        public AssignTicketsRequest Build() => _request;
    }
}