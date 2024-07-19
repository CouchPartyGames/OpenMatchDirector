using OpenMatchDirector.Observability.Options;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace OpenMatchDirector.Observability.Dependency;

public static class LoggingInjection
{
    public static ILoggingBuilder AddObservabilityLogging(this ILoggingBuilder loggingBuilder,
        IConfiguration configuration,
        ResourceBuilder resourceBuilder)
    {
        var options = configuration
            .GetSection(OpenTelemetryOptions.SectionName)
            .Get<OpenTelemetryOptions>();
        
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddOpenTelemetry(opts =>
        {
            opts.SetResourceBuilder(resourceBuilder);
            opts.IncludeScopes = true;
            opts.IncludeFormattedMessage = true;
            opts.AddOtlpExporter(export =>
            {
                export.Endpoint = new Uri(OpenTelemetryOptions.OtelDefaultHost);
                export.Protocol = OtlpExportProtocol.Grpc;
            });
        });
        
        return loggingBuilder;
    }
    
    public static IServiceCollection AddObservabilityLogging(this IServiceCollection services,
        IConfiguration configuration,
        ResourceBuilder resourceBuilder)
    {
        var options = configuration
            .GetSection(OpenTelemetryOptions.SectionName)
            .Get<OpenTelemetryOptions>();

        services.AddOpenTelemetry().WithLogging(logging =>
            {
                logging.SetResourceBuilder(resourceBuilder);
                logging.AddConsoleExporter();
                logging.AddOtlpExporter(export =>
                {
                    export.Endpoint = new Uri(OpenTelemetryOptions.OtelDefaultHost);
                    export.Protocol = OtlpExportProtocol.Grpc;
                });
            },
            logOptions =>
            {
                logOptions.IncludeFormattedMessage = true;
                logOptions.IncludeScopes = true;
                logOptions.ParseStateValues = true;
            });

        return services;
    }
}