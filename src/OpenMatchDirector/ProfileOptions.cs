namespace OpenMatchDirector;

public class ProfileOptions
{
    public const string SectionName = "Profile";

    public const string OpenMatchFunctionDefaultHost = "openmatch-function.open-match.svc.cluster.local";

    public const int OpenMatchFunctionDefaultPort = 50888;

    // Profile Name
    public string Name { get; init; } = "";

    // Match Function Host
    public string Host { get; init; } = OpenMatchFunctionDefaultHost;
    
    // Match Function Port
    public int Port { get; init; } = OpenMatchFunctionDefaultPort;
    
    // gRPC or REST
    public bool IsGrpcProtocol { get; init; } = false;
}