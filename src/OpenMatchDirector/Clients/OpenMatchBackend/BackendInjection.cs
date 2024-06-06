using Grpc.Net.ClientFactory;
using OpenMatchDirector.Interceptors;
using OpenMatchDirector.Options;

namespace OpenMatchDirector.Clients.OpenMatchBackend;

public static class BackendInjection
{
    public static IServiceCollection AddBackendClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OpenMatchOptions>(configuration.GetSection(OpenMatchOptions.SectionName));
        
        services
            .AddGrpcClient<BackendService.BackendServiceClient>(o =>
            {
                var address = configuration["OPENMATCH_BACKEND_HOST"] ??
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
        
        return services;
    }
}