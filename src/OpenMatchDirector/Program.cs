using Microsoft.Extensions.Http.Resilience;
using OpenMatchDirector;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<HostOptions>(o =>
{
    o.ShutdownTimeout = TimeSpan.FromSeconds(15);
    o.ServicesStartConcurrently = true;
    o.ServicesStopConcurrently = true;
});

builder.Services.AddGrpcClient<QueryService.QueryServiceClient>(o =>
{
    var address = builder.Configuration["OPENMATCH_QUERY_HOST"] ??
                  "https://open-match-query.open-match.svc.cluster.local:50503";
    o.Address = new Uri(address);
}).AddStandardResilienceHandler();

builder.Services.AddGrpcClient<BackendService.BackendServiceClient>(o =>
{
    var address = builder.Configuration["OPENMATCH_BACKEND_HOST"] ??
                  "https://open-match-backend.open-match.svc.cluster.local:50505";
    o.Address = new Uri(address);
}).AddStandardResilienceHandler();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();