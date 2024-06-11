using Microsoft.Extensions.Options;

namespace OpenMatchDirector.Clients.Agones.Options;

public sealed class AgonesOptions
{
    public const string SectionName = "Agones";

    public string Name { get; init; } = "Agones Virginia";
    
    public string Address { get; init; } = "us1.agones.couchparty.games";
    
    public int Port { get; init; } = 443;
}

public sealed class AllocationList
{
    [ValidateEnumeratedItems]
    public List<AgonesOptions> AgonesEndpoints { get; set; }
}