namespace OpenMatchDirector.Options;

public sealed class MatchFunctionOptions
{
    public const string SectionName = "MatchFunctions";
    
    public string Name { get; init; }
    public string Address { get; init; }
    public int Port { get; init; }
}