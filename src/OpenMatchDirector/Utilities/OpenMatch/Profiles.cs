using OpenMatchDirector.Utilities.Profiles;

namespace OpenMatchDirector.Utilities.OpenMatch;


public sealed class Profiles
{
    private List<ProfileFunctionMap> _profiles = new();
    
    public List<ProfileFunctionMap> GenerateProfiles()
    {
        var defaultProfile = new MatchProfile
        {
            Name = "default-profile"
        };
        var defaultFunc = FunctionHelper.NewFunctionConfig("test", 5505);
        _profiles.Add(new ProfileFunctionMap(defaultProfile, defaultFunc));
        
        return _profiles;
    }

    public sealed class PoolBuilder
    {
        private Pool _pool = new();
        
        public PoolBuilder WithName(string name)
        {
             _pool.Name = name;
             return this;
        }

        public PoolBuilder AddTags(TagFilter filter)
        {
            _pool.TagPresentFilters.Add(new TagPresentFilter
            {
                Tag = filter.Value
            });
            return this;
        }

        public PoolBuilder AddStringFilters(StringFilter filter)
        {
            _pool.StringEqualsFilters.Add(new StringEqualsFilter
            {
                StringArg = filter.Key,
                Value = filter.Value
            });
            return this;
        }

        public PoolBuilder AddRangeFilters(DoubleFilter filter)
        {
            _pool.DoubleRangeFilters.Add(new DoubleRangeFilter 
            {
                DoubleArg = filter.Name,
                Min = filter.Min,
                Max = filter.Max
            });
            return this;
        }

        public Pool Build() => _pool;
    }


    public sealed record DoubleFilter(string Name, double Min, double Max);
        
    public sealed record StringFilter(string Key, string Value);
    
    public sealed record TagFilter(string Value);
}
