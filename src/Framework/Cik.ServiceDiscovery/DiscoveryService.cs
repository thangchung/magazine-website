using System.Threading.Tasks;
using Microphone.Core;
using Microphone.Core.ClusterProviders;
using Microsoft.Extensions.Configuration;

namespace Cik.ServiceDiscovery
{
  public abstract class DiscoveryService : IDiscoveryService
  {
    protected DiscoveryService()
    {
      var builder = new ConfigurationBuilder()
        .AddJsonFile("consul.json");

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public abstract void BootstrapClient();

    public async Task<ServiceInformation[]> FindServiceInstancesAsync(string name)
    {
      return await Cluster.FindServiceInstancesAsync(name);
    }

    public async Task<ServiceInformation> FindServiceInstanceAsync(string name)
    {
      return await Cluster.FindServiceInstanceAsync(name);
    }

    public void RegisterService(IClusterProvider clusterProvider)
    {
      Cluster.BootstrapClient(clusterProvider);
    }

    public void RegisterService(IFrameworkProvider frameworkProvider, IClusterProvider clusterProvider,
      string serviceName, string version)
    {
      Cluster.Bootstrap(frameworkProvider, clusterProvider, serviceName, version);
    }

    public async Task KeyValuePutAsync(string key, object value)
    {
      await Cluster.KeyValuePutAsync(key, value);
    }

    public async Task<T> KeyValueGetAsync<T>(string key)
    {
      return await Cluster.KeyValueGetAsync<T>(key);
    }
  }
}