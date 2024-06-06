using OpenTelemetry.Resources;

namespace OpenMatchDirector.Observability;

public static class OtelResourceBuilder
{
    public static ResourceBuilder ResourceBuilder { get; } = ResourceBuilder
        .CreateDefault()
        .AddService("OpenMatchDirector", null, "1.0.0")
        .AddTelemetrySdk();    
}