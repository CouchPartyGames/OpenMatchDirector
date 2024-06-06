namespace OpenMatchDirector.Options;

public sealed class OpenMatchOptions
{
    public const string SectionName = "OpenMatch";

    public string QueryServiceAddr { get; set; } = "";

    public string BackendServiceAddr { get; init; } = "http://open-match-backend.open-match.svc.cluster.local:50505";
}