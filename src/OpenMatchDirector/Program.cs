using Grpc.Net.Client.Balancer;
using OpenMatchDirector;
using OpenMatchDirector.Clients.Agones;
using OpenMatchDirector.Clients.OpenMatchBackend;
using OpenMatchDirector.Interceptors;
using OpenMatchDirector.Observability;
using OpenMatchDirector.Options;
using OpenMatchDirector.Profiles;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .Configure<OpenMatchOptions>(builder.Configuration.GetSection(OpenMatchOptions.SectionName));

    // Observability
builder.Logging.AddObservabilityLogging(builder.Configuration, OtelResourceBuilder.ResourceBuilder);
builder.Services.AddObservabilityMetrics(builder.Configuration, OtelResourceBuilder.ResourceBuilder);
builder.Services.AddObservabilityTracing(builder.Configuration, OtelResourceBuilder.ResourceBuilder);

    // Clients
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

builder.Services.AddSingleton<ResolverFactory>(
    sp => new DnsResolverFactory(refreshInterval: TimeSpan.FromSeconds(30)));
builder.Services.AddTransient<ExceptionInterceptor>();
builder.Services.AddSingleton<IProfileFunctionMap>(defaultProfile);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();