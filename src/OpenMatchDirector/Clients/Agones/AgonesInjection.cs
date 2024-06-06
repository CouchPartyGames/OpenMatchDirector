namespace OpenMatchDirector.Clients.Agones;

public static class AgonesInjection
{
    public static IServiceCollection AddAgonesClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}