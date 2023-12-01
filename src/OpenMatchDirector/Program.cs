using Grpc.Net.Client.Balancer;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Http.Resilience;
using OpenMatchDirector;
using OpenMatchDirector.Interceptors;
using OpenMatchDirector.Profiles;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<HostOptions>(o =>
{
    o.ShutdownTimeout = TimeSpan.FromSeconds(15);
    o.ServicesStartConcurrently = true;
    o.ServicesStopConcurrently = true;
    o.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

builder.Services
    .AddGrpcClient<BackendService.BackendServiceClient>(o =>
    {
        var address = builder.Configuration["OPENMATCH_BACKEND_HOST"] ??
                      "https://open-match-backend.open-match.svc.cluster.local:50505";
        o.Address = new Uri(address);
    })
    .AddInterceptor<ExceptionInterceptor>(InterceptorScope.Channel)
    .AddStandardResilienceHandler();



var defaultProfile = new DefaultProfiles();

builder.Services.AddSingleton<ResolverFactory>(
    sp => new DnsResolverFactory(refreshInterval: TimeSpan.FromSeconds(30)));
builder.Services.AddTransient<ExceptionInterceptor>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IProfileFunctionMap>(defaultProfile);

var host = builder.Build();
host.Run();