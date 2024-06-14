using Allocation;
using Grpc.Net.ClientFactory;
using OpenMatchDirector.Clients.Agones.Options;
using OpenMatchDirector.Interceptors;

namespace OpenMatchDirector.Clients.Agones.Dependency;

public static class AgonesInjection
{
    public static IServiceCollection AddAgonesClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = services.Configure<AgonesOptions>(configuration.GetSection(AgonesOptions.SectionName));

        services
            .AddGrpcClient<AllocationService.AllocationServiceClient>(o =>
            {
                var address = "http://";
                o.Address = new Uri(address);
            })
            .AddInterceptor<ExceptionInterceptor>(InterceptorScope.Channel)
            .AddStandardResilienceHandler();
        
        return services;
    }
}