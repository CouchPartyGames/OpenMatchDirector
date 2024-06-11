namespace OpenMatchDirector.Utilities.OpenMatch;

public sealed class MatchHelper
{
    public static async Task<bool> Fetch(BackendService.BackendServiceClient client, FetchMatchesRequest request)
    {
        var response = client.FetchMatches(request);
        return true;
    }


    public sealed class RequestBuilder
    {
        private readonly FetchMatchesRequest _fetchMatchesRequest = new();

        public RequestBuilder WithMatchProfile(MatchProfile profile)
        {
            _fetchMatchesRequest.Profile = profile;
             return this;
        }

        public RequestBuilder WithFunctionConfig(FunctionConfig config)
        {
            _fetchMatchesRequest.Config = config;
            return this;
        }
        
        public FetchMatchesRequest Build() => _fetchMatchesRequest;
    }

}