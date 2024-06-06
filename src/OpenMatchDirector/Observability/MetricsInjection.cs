using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace OpenMatchDirector.Observability;

public static class MetricsInjection
{
    public static IServiceCollection AddObservabilityMetrics(this IServiceCollection services,
        IConfiguration configuration,
        ResourceBuilder resourceBuilder)
    {
        var openTelemetryOptions = configuration
            .GetSection(OpenTelemetryOptions.SectionName)
            .Get<OpenTelemetryOptions>();
        
        const OtlpExportProtocol otelProtocol = OtlpExportProtocol.Grpc;
        const string endpoint = OpenTelemetryOptions.OtelDefaultHost;
        
        services.AddOpenTelemetry()
            .WithMetrics(opts =>
            {
                opts.SetResourceBuilder(resourceBuilder);
                opts.AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();

                opts.AddOtlpExporter(export =>
                {
                    export.Endpoint = new Uri(endpoint);
                    export.Protocol = otelProtocol;
                });
            });
        
        return services;
    }

}