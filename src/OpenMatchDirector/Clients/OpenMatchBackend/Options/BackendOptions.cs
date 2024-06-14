namespace OpenMatchDirector.Clients.OpenMatchBackend.Options;

public sealed class BackendOptions
{
    public const string SectionName = "OpenMatchBackend";

    public const string OpenMatchBackendDefaultAddress = "http://open-match-backend.open-match.svc.cluster.local:50505";

    public string BackendServiceAddress { get; init; } = OpenMatchBackendDefaultAddress;
}