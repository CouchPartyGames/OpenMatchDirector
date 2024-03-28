using Grpc.Net.Client.Balancer;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Http.Resilience;
using OpenMatchDirector;
using OpenMatchDirector.Interceptors;
using OpenMatchDirector.Options;
using OpenMatchDirector.Profiles;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(opts =>
{
    opts.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OpenMatchDirector"));
    opts.IncludeScopes = true;
    opts.IncludeFormattedMessage = true;
    opts.AddOtlpExporter(export =>
    {
        export.Endpoint = new Uri("http://localhost:4317");
    });
});
builder.Services.Configure<HostOptions>(o =>
{
    o.ShutdownTimeout = TimeSpan.FromSeconds(15);
    o.ServicesStartConcurrently = true;
    o.ServicesStopConcurrently = true;
    o.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
/*
builder.Services
    .AddOptions<AllocationList>()
    .Bind(builder.Configuration, options =>
    {
        options.ErrorOnUnknownConfiguration = true;
    })
    .ValidateDataAnnotations()
    .ValidateOnStart();
*/

builder.Services
    .AddGrpcClient<BackendService.BackendServiceClient>(o =>
    {
        var address = builder.Configuration["OPENMATCH_BACKEND_HOST"] ??
                      "http://open-match-backend.open-match.svc.cluster.local:50505";
        o.Address = new Uri(address);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        return handler;
    })
    .AddInterceptor<ExceptionInterceptor>(InterceptorScope.Channel)
    .AddStandardResilienceHandler();

builder.Services.AddOpenTelemetry()
    .WithMetrics(opts =>
    {
        opts.AddRuntimeInstrumentation()
            .AddHttpClientInstrumentation();
        
        opts.AddOtlpExporter(export =>
        {
            export.Endpoint = new Uri("http://localhost:4317");
        });
    })
    .WithTracing(opts =>
    {
        opts.AddHttpClientInstrumentation();
        opts.AddOtlpExporter(export =>
        {
            export.Endpoint = new Uri("http://localhost:4317");
        });
    });


var defaultProfile = new DefaultProfiles();

builder.Services.AddSingleton<ResolverFactory>(
    sp => new DnsResolverFactory(refreshInterval: TimeSpan.FromSeconds(30)));
builder.Services.AddTransient<ExceptionInterceptor>();
builder.Services.AddSingleton<IProfileFunctionMap>(defaultProfile);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();