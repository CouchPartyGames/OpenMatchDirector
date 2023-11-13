namespace OpenMatchDirector.OpenMatch;

public sealed class FetchMatches(BackendService.BackendServiceClient _client)
{
    public bool Fetch(BackendService.BackendServiceClient client, FetchMatchesRequest request)
    {
        var response = _client.FetchMatches(request);
        return true;
    }

    // Connection details for the backend service to determine what match maker should be used
    public static FunctionConfig CreateFunctionConfig(string host, int port, bool isGrpc)
    {
        var type = isGrpc ? FunctionConfig.Types.Type.Grpc : FunctionConfig.Types.Type.Rest;
        return new FunctionConfig
        {
            Host = host,
            Port = port,
            Type = type
        };
    }

    public sealed class RequestBuilder
    {
        private MatchProfile _profile = new();
        private FunctionConfig _config = new();

        public RequestBuilder WithMatchProfile(MatchProfile profile)
        {
             _profile = profile;
             return this;
        }

        public RequestBuilder WithFunctionConfig(FunctionConfig config)
        {
            _config = config;
            return this;
        }
        
        public FetchMatchesRequest Build()
        {
                // https://openmatch.dev/site/docs/reference/api/#fetchmatchesrequest
            return new FetchMatchesRequest
            {
                Config = _config, 
                Profile = _profile
            };
        }
    }

}