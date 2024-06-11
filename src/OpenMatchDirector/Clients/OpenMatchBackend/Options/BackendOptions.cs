namespace OpenMatchDirector.Clients.OpenMatchBackend;

public sealed class BackendOptions
{
    public const string SectionName = "Backend";

    public const string OpenMatchBackendDefaultHost = "open-match-backend.open-match.svc.cluster.local";
    public const int OpenMatchBackendDefaultPort = 50505;

    public string BackendServiceAddr { get; init; } = "http://open-match-backend.open-match.svc.cluster.local:50505";
}