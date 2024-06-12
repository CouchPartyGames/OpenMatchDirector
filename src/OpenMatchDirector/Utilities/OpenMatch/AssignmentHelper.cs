namespace OpenMatchDirector.Utilities.OpenMatch;

public static class AssignmentHelper
{
    
    public static Assignment NewAssignment(string address, int port) 
        => new Assignment { Connection = $"{address}:{port}" };

    public static AssignmentGroup NewAssignmentGroup(List<Ticket> tickets, Assignment assignment)
    {
        var list = tickets.Select(x => x.Id);
        return new AssignmentGroup
        {
            TicketIds = { list },
            Assignment = assignment
        };
    }
    
    
    public static AssignmentGroup NewAssignmentGroup(RepeatedField<string> tickets, string address, int port)
        => NewAssignmentGroup(tickets, NewAssignment(address, port));
    
    public static AssignmentGroup NewAssignmentGroup(RepeatedField<string> tickets, Assignment assignment) 
        => new AssignmentGroup { TicketIds = { tickets }, Assignment = assignment };

    
    
    public sealed class RequestBuilder
    {
        private readonly AssignTicketsRequest _request = new();
        
        public RequestBuilder WithAssignmentGroup(AssignmentGroup group)
        {
            _request.Assignments.Add(group);
            return this;
        }

        public RequestBuilder WithAssignmentGroupList(IEnumerable<AssignmentGroup> list)
        {
            _request.Assignments.AddRange(list);
            return this;
        }

        public AssignTicketsRequest Build() => _request;
    }
}