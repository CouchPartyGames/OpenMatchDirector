namespace OpenMatchDirector.Utilities.OpenMatch;

public static class FunctionHelper
{
        // Connection details for the backend service to determine what match maker should be used
    public static FunctionConfig NewFunctionConfig(string host, int port, FunctionConfig.Types.Type type)
        => new FunctionConfig { Host = host, Port = port, Type = type };
    
    public static FunctionConfig NewFunctionConfig(string host, int port, bool isGrpc = true)
    {
        var type = isGrpc ? FunctionConfig.Types.Type.Grpc : FunctionConfig.Types.Type.Rest;
        return NewFunctionConfig(host, port, type);
    }
}