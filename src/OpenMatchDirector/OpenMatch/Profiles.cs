namespace OpenMatchDirector.OpenMatch;

public interface IMode
{
    List<Profiles.Mode> Modes { get; }
}

public sealed class DefaultModes : IMode
{
    public List<Profiles.Mode> Modes { get; } = new()
    {
        new Profiles.Mode("mode.ctf"),
        new Profiles.Mode("mode.tournament")
    };
}

public sealed class Profiles
{
    private IMode _mode;
    public Profiles(IMode mode) => _mode = mode;
    
    public List<Mode> Modes { get; } = new() { new Mode("mode.ctf") };
    
    public RepeatedField<MatchProfile> GenerateProfiles()
    { 
        var profiles = new RepeatedField<MatchProfile>();
        foreach(var mode in Modes)
        {

            var poolName = $"pool_{mode.Value}";
            
            /*
            Pool pool = new PoolBuilder()
                .WithName(poolName)
                .AddTags()
                .AddStringFilters()
                .AddRangeFilters()
                .Build();
                */
            
            //mode.Value;
            Pool pool = new Pool
            {
                Name = poolName,
                TagPresentFilters = { },
                StringEqualsFilters = { }
            };

            var profile = new MatchProfile();
            profile.Name = $"profile_{mode.Value}";
            profile.Pools.Add(pool);
            
            profiles.Add(profile);
        }

        return profiles;
    }

    public class PoolBuilder
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
    
    public sealed record Mode(string Value);
}