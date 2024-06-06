namespace OpenMatchDirector.Observability;

public sealed class OpenTelemetryOptions
{
    public const string SectionName = "OpenTelemetry";

    public const string OtelDefaultHost = "http://localhost:4317";

    public string Endpoint { get; init; } = OtelDefaultHost;
}