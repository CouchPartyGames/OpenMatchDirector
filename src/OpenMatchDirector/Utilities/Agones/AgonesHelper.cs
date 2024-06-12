namespace OpenMatchDirector.Utilities.Agones;

public static class AgonesHelper
{
    public record AgonesHost(string Address, int Port)
    {
        public override string ToString() => $"{Address}:{Port}";
    }

    public static AgonesHost NewFakeHost() => new AgonesHost("192.168.1.1", 5000);
}