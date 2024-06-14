using Grpc.Net.ClientFactory;
using OpenMatchDirector.Clients.OpenMatchBackend.Options;
using OpenMatchDirector.Interceptors;

namespace OpenMatchDirector.Clients.OpenMatchBackend;

public static class BackendInjection
{
    public static IServiceCollection AddBackendClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = services.Configure<BackendOptions>(configuration.GetSection(BackendOptions.SectionName));
        
        services
            .AddGrpcClient<BackendService.BackendServiceClient>(o =>
            {
                var address = configuration["OPENMATCH_BACKEND_HOST"] ??
                              BackendOptions.OpenMatchBackendDefaultAddress; 
                o.Address = new Uri(address);
            })
            /*.ConfigureChannel(o =>
            {
                o.HttpHandler = new SocketsHttpHandler()
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    EnableMultipleHttp2Connections = true
                };
                o.MaxRetryAttempts = 4;
            })*/
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                return handler;
            })
            .AddInterceptor<ExceptionInterceptor>(InterceptorScope.Channel)
            .AddStandardResilienceHandler();
        
        services.AddTransient<ExceptionInterceptor>();
        
        return services;
    }
}