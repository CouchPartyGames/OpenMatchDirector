using Microsoft.Extensions.Options;

namespace OpenMatchDirector.Clients.Agones;

public sealed class AllocationOptions
{
    public const string SectionName = "Allocations";
    
    public string Name { get; init; }
    public string Address { get; init; }
    public int Port { get; init; }
}

public sealed class AllocationList
{
    [ValidateEnumeratedItems]
    public List<AllocationOptions> Allocations { get; set; }
}