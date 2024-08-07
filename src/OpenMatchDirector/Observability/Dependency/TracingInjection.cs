using OpenMatchDirector.Observability.Options;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenMatchDirector.Observability.Dependency;

public static class TracingInjection
{
    public static IServiceCollection AddObservabilityTracing(this IServiceCollection services,
        IConfiguration configuration,
        ResourceBuilder resourceBuilder)
    {
        var openTelemetryOptions = configuration
            .GetSection(OpenTelemetryOptions.SectionName)
            .Get<OpenTelemetryOptions>();
        
        const float samplingRate = 1.0f;
        const string endpoint = OpenTelemetryOptions.OtelDefaultHost;
        const OtlpExportProtocol otelProtocol = OtlpExportProtocol.Grpc;
        
        services.AddOpenTelemetry()
            .WithTracing(opts =>
            {
                opts.SetResourceBuilder(resourceBuilder);
                opts.SetSampler(new TraceIdRatioBasedSampler(samplingRate));
                opts.AddHttpClientInstrumentation()
                    .AddGrpcClientInstrumentation();
                
                opts.AddOtlpExporter(export =>
                {
                    export.Endpoint = new Uri(endpoint);
                    export.Protocol = otelProtocol;
                });
            });
        
        return services;
    }
}