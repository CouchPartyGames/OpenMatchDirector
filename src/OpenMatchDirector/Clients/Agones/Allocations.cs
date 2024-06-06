using Allocation;

namespace OpenMatchDirector.Clients.Agones;

public sealed class ServerAllocations
{
    
    public sealed class RequestBuilder
    {
        private string _namespace = "default";
        private bool _multiCluster = false;
            
        public RequestBuilder WithNamespace(string ns) {
            _namespace = ns;
            return this;
        }

        public RequestBuilder WithGameSelectors(Dictionary<string, string> labels) {
            return this;
        }

        public RequestBuilder WithMetadata(Dictionary<string, string> labels, Dictionary<string, string> annotations) {
            return this;
        }

        public RequestBuilder WithMultiCluster(bool isEnabled)
        {
            _multiCluster = isEnabled;
            return this;
        }

        
        public AllocationRequest Build() {
            return new AllocationRequest {
                Namespace = _namespace,
                Metadata = {},
                GameServerSelectors = {  },
                MultiClusterSetting = {}
            };
        }
    }


    public sealed record AllocationEndpoint(string Address, int Port);
}