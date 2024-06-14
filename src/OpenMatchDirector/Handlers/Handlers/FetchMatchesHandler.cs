using OpenMatchDirector.Utilities.OpenMatch;
using OpenMatchDirector.Utilities.Profiles;

namespace OpenMatchDirector.Handlers.Handlers;

public class FetchMatchesHandler(
    BackendService.BackendServiceClient backendClient,
    CancellationToken token)
{

    public async Task Handler(ProfileFunctionMap profileFunctionMap)
    {
        FetchMatchesRequest request = new MatchHelper.RequestBuilder()
            .WithFunctionConfig(profileFunctionMap.Function)
            .WithMatchProfile(profileFunctionMap.Profile)
            .Build();

        using var call = backendClient.FetchMatches(request, cancellationToken: token);
        var response = call.ResponseStream.ReadAllAsync(token);
        
        
    }
}