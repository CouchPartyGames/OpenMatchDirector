namespace OpenMatchDirector.Utilities.Profiles;

public class DefaultProfiles : IProfileFunctionMap
{
    public List<ProfileFunctionMap> GenerateProfiles()
    {
        var defaultProfile = new MatchProfile
        {
            Name = "default-profile"
        };
        var defaultFunc = Utilities.OpenMatch.Profiles.CreateFunctionConfig("test", 5505);
        
        //kvar func = Create
        var map = new ProfileFunctionMap(defaultProfile, defaultFunc);
        return new List<ProfileFunctionMap>()
        {
            map
        };
    }
}