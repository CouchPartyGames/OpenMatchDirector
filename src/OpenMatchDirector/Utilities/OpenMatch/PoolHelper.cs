using Google.Protobuf.WellKnownTypes;

namespace OpenMatchDirector.Utilities.OpenMatch;

public sealed class PoolHelper
{
    
    public sealed class PoolBuilder
    {
        private readonly Pool _pool = new();
        
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

        public PoolBuilder AddCreatedBefore(Timestamp timestamp)
        {
            _pool.CreatedBefore = timestamp;
            return this;
        }

        public PoolBuilder AddCreatedAfter(Timestamp timestamp)
        {
            _pool.CreatedAfter = timestamp;
            return this;
        }

        public Pool Build() => _pool;
    }


    public sealed record DoubleFilter(string Name, double Min, double Max);
        
    public sealed record StringFilter(string Key, string Value);
    
    public sealed record TagFilter(string Value);
}