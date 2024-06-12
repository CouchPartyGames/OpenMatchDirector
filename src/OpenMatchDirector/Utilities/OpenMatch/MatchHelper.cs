using Grpc.Core;

namespace OpenMatchDirector.Utilities.OpenMatch;

public sealed class MatchHelper
{
    /*
    public static async Task Fetch(BackendService.BackendServiceClient client, FetchMatchesRequest request)
    {
        var response = client.FetchMatches(request);
        //var readAllAsync = response.ResponseStream.ReadAllAsync();
        //readAllAsync
        return response;
    }*/


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