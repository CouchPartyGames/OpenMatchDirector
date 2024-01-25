using Director.Agones;

namespace OpenMatchDirector.OpenMatch;

public sealed class Assign
{
    public async Task<bool> AssignTickets(BackendService.BackendServiceClient client, AssignTicketsRequest request)
    {
        var response  =  await client.AssignTicketsAsync(request);
        return true;
    }

    public static Assignment CreateAssignment(string addr, int port) => new Assignment { Connection = $"{addr}:{port}" };
    
    
    public sealed class RequestBuilder
    {
        private AssignTicketsRequest _request = new();

        public RequestBuilder WithAssignment(string address, int port)
        {
            new Assignment {
                Connection = $"address:port"
            };
            return this;
        }

        public RequestBuilder WithTicketIds(List<Ticket> tickets)
        {
            return this;
        }
        public RequestBuilder WithTicketIds(RepeatedField<Ticket> tickets)
        {
            return this;
        }
        
        public RequestBuilder WithAssignmentGroup(AssignmentGroup group)
        {
            _request.Assignments.Add(group);
            return this;
        }

        public AssignTicketsRequest Build() => _request;
    }

}