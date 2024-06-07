using OpenTelemetry.Resources;

namespace OpenMatchDirector.Observability;

public static class OtelResourceBuilder
{
    public static ResourceBuilder ResourceBuilder { get; } = ResourceBuilder
        .CreateDefault()
        .AddService(GlobalConsts.ServiceName, null, GlobalConsts.ServiceVersion)
        .AddTelemetrySdk();    
}