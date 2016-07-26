using System.Threading.Tasks;
using Microphone;

namespace Cik.ServiceDiscovery
{
    public abstract class DiscoveryService : IDiscoveryService
    {
        private readonly IClusterClient _client;

        protected DiscoveryService(IClusterClient client)
        {
            _client = client;
        }

        public async Task<ServiceInformation[]> GetServiceInstancesAsync(string name)
        {
            return await _client.GetServiceInstancesAsync(name);
        }

        public async Task KeyValuePutAsync(string key, object value)
        {
            await Cluster.Client.KeyValuePutAsync(key, value);
        }

        public async Task<T> KeyValueGetAsync<T>(string key)
        {
            return await Cluster.Client.KeyValueGetAsync<T>(key);
        }

        public abstract void BootstrapClient();
    }
}