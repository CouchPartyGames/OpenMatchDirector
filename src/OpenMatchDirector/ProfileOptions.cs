namespace OpenMatchDirector;

public class ProfileOptions
{
    public const string SectionName = "Profile";

    // Profile Name
    public string Name { get; init; } = "";

    // Match Function Host
    public string Host { get; init; } = "";
    
    // Match Function Port
    public int Port { get; init; } = 0;
    
    // gRPC or REST
    public bool IsGrpcProtocol { get; init; } = false;
}