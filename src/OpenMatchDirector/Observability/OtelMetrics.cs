using System.Diagnostics.Metrics;

namespace OpenMatchDirector.Observability;

public sealed class OtelMetrics
{
    private const string Name = "Director";

    private const string AssignmentBase = "director.assignment";
    
    private const string AllocationBase = "director.allocation";
    
    private readonly Counter<int> _assignSuccesses;
    private readonly Counter<int> _assignFailures;
    private readonly Counter<int> _assignFailuresUnknown;
    private readonly Counter<int> _assignFailuresTicketNotFound;
   
    private readonly Counter<int> _allocationSuccesses;
    private readonly Counter<int> _allocationFailures;
    
    public OtelMetrics(IMeterFactory factory)
    {
        var meter = factory.Create(Name);
        _assignSuccesses = meter.CreateCounter<int>(AssignmentBase + ".success.count");
        _assignFailures = meter.CreateCounter<int>(AssignmentBase + ".failure.count");
        _assignFailuresUnknown = meter.CreateCounter<int>(AssignmentBase + ".failure.unknown.count");
        _assignFailuresTicketNotFound = meter.CreateCounter<int>(AssignmentBase + ".failure.ticketnotfound.count");
        
        _allocationSuccesses = meter.CreateCounter<int>(AllocationBase + ".success.count");
        _allocationFailures = meter.CreateCounter<int>(AllocationBase + ".failure.count");
    }

    
    public void AddAssignmentSuccess(int total = 1) => _assignSuccesses.Add(total);

    public void AddAssignmentFailureUnknown(int total = 1)
    { 
        _assignFailuresUnknown.Add(total);
        _assignFailures.Add(total);
    }

    public void AddAssignmentFailureNotFound(int total = 1)
    {
        _assignFailuresTicketNotFound.Add(total);
        _assignFailures.Add(total);
    } 
    
    
    public void AddAllocationSuccess(int total = 1) => _allocationSuccesses.Add(total);
    public void AddAllocationFailure(int total = 1) => _allocationFailures.Add(total);
}