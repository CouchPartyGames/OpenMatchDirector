using Grpc.Net.Client.Balancer;
using OpenMatchDirector;
using OpenMatchDirector.Clients.Agones.Dependency;
using OpenMatchDirector.Clients.OpenMatchBackend;
using OpenMatchDirector.Interceptors;
using OpenMatchDirector.Observability;
using OpenMatchDirector.Observability.Dependency;
using OpenMatchDirector.Utilities.Profiles;

var builder = Host.CreateApplicationBuilder(args);

    // Observability
builder.Logging.AddObservabilityLogging(builder.Configuration, OtelResourceBuilder.ResourceBuilder);
builder.Services.AddObservabilityMetrics(builder.Configuration, OtelResourceBuilder.ResourceBuilder);
builder.Services.AddObservabilityTracing(builder.Configuration, OtelResourceBuilder.ResourceBuilder);

    // Clients
builder.Services.AddSingleton<ResolverFactory>(
    sp => new DnsResolverFactory(refreshInterval: TimeSpan.FromSeconds(30)));
builder.Services.AddBackendClient(builder.Configuration);
builder.Services.AddAgonesClient(builder.Configuration);


    // Service (Background)
builder.Services.Configure<HostOptions>(o =>
{
    o.ShutdownTimeout = TimeSpan.FromSeconds(15);
    o.ServicesStartConcurrently = true;
    o.ServicesStopConcurrently = true;
    o.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
var defaultProfile = new DefaultProfiles();
builder.Services.AddSingleton<IProfileFunctionMap>(defaultProfile);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();