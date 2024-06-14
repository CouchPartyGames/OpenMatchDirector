using OpenMatchDirector.Utilities.OpenMatch;

namespace OpenMatchDirector.Utilities.Profiles;

public class DefaultProfiles : IProfileFunctionMap
{
    public List<ProfileFunctionMap> GenerateProfiles()
    {
        var pool = new OpenMatch.Profiles.PoolBuilder()
            .WithName("default-pool")
            .Build();
        var defaultProfile = new MatchProfile
        {
            Name = "default-profile",
            Pools = { pool }
        };
        var defaultFunc = FunctionHelper.NewFunctionConfig(ProfileOptions.OpenMatchFunctionDefaultHost, 
            ProfileOptions.OpenMatchFunctionDefaultPort);
        
        return new List<ProfileFunctionMap>()
        {
            new(defaultProfile, defaultFunc)
        };
    }
}