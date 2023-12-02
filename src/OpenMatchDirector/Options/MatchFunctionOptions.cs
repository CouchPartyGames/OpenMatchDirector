namespace OpenMatchDirector.Options;

public class MatchFunctionOptions
{
    public const string SectionName = "MatchFunctions";
    
    public string Name { get; }
    public string Address { get; }
    public int Port { get; }
}