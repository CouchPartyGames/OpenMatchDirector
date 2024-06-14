using Allocation;

namespace OpenMatchDirector.Clients.Agones;

public sealed class ServerAllocations
{
    
    public sealed class RequestBuilder
    {
        private AllocationRequest _allocationRequest = new();
        private string _namespace = "default";
        private bool _multiCluster = false;
            
        public RequestBuilder WithNamespace(string @namespace)
        {
            _allocationRequest.Namespace = @namespace;
            return this;
        }

        public RequestBuilder WithGameSelectors(Dictionary<string, string> selectors)
        {
            //_allocationRequest.GameServerSelectors.AddRange(labels);
            return this;
        }

        public RequestBuilder WithMetadata(Dictionary<string, string> labels, Dictionary<string, string> annotations)
        {
            //_allocationRequest.Metadata.Labels = { labels } ;
            //_allocationRequest.Metadata.Annotations = annotations;
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