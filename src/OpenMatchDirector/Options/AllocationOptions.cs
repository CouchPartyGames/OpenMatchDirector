using Microsoft.Extensions.Options;

namespace OpenMatchDirector.Options;

public sealed class AllocationOptions
{
    public const string SectionName = "Allocations";
    
    public string Name { get; }
    public string Address { get; }
    public int Port { get; }
}

public sealed class AllocationList
{
    [ValidateEnumeratedItems]
    public List<AllocationOptions> Allocations { get; set; }
}