namespace OpenMatchDirector.Clients.Agones.Dependency;

public static class AgonesInjection
{
    public static IServiceCollection AddAgonesClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}