namespace OpenMatchDirector.Options;

public sealed class TelemetryOptions
{
   public const string SectionName = "Telemetry";
   
   public bool Enabled { get; init; } = false;
   
   public string Endpoint { get; init; }
}