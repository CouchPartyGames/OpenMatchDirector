using Microsoft.Extensions.Options;

namespace OpenMatchDirector.Clients.Agones;

public sealed class AgonesOptions
{
    public const string SectionName = "Agones";

    public string Name { get; init; } = "Agones";
    public string Address { get; init; }
    public int Port { get; init; } = 443;
}

public sealed class AllocationList
{
    [ValidateEnumeratedItems]
    public List<AgonesOptions> AgonesEndpoints { get; set; }
}