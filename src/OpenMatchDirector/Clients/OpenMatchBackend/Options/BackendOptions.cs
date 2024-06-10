namespace OpenMatchDirector.Clients.OpenMatchBackend;

public sealed class BackendOptions
{
    public const string SectionName = "Backend";

    public string BackendServiceAddr { get; init; } = "http://open-match-backend.open-match.svc.cluster.local:50505";
}