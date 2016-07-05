using System.Threading.Tasks;
using Microphone.Core;
using Microphone.Core.ClusterProviders;

namespace Cik.ServiceDiscovery
{
  public interface IServiceDiscovery
  {
    Task<ServiceInformation[]> FindServiceInstancesAsync(string name);
    Task<ServiceInformation> FindServiceInstanceAsync(string name);
    void RegisterService(IClusterProvider clusterProvider);

    void RegisterService(IFrameworkProvider frameworkProvider, IClusterProvider clusterProvider,
      string serviceName, string version);

    Task KeyValuePutAsync(string key, object value);
    Task<T> KeyValueGetAsync<T>(string key);
  }
}