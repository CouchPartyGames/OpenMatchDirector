using OpenMatchDirector.Utilities.OpenMatch;

namespace OpenMatchDirector.Utilities.Profiles;

public class DefaultProfiles : IProfileFunctionMap
{
    public List<ProfileFunctionMap> GenerateProfiles()
    {
        var defaultProfile = new MatchProfile
        {
            Name = "default-profile"
        };
        var defaultFunc = FunctionHelper.NewFunctionConfig(ProfileOptions.OpenMatchFunctionDefaultHost, 
            ProfileOptions.OpenMatchFunctionDefaultPort);
        
        return new List<ProfileFunctionMap>()
        {
            new(defaultProfile, defaultFunc)
        };
    }
}