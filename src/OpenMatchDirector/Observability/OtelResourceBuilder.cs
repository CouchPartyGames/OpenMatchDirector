using OpenTelemetry.Resources;

namespace OpenMatchDirector.Observability;

public static class OtelResourceBuilder
{
    public static ResourceBuilder ResourceBuilder { get; } = ResourceBuilder
        .CreateDefault()
        .AddService(GlobalConstants.ServiceName, null, GlobalConstants.ServiceVersion)
        .AddTelemetrySdk();    
}