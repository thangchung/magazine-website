using System.Threading.Tasks;
using Microphone;

namespace Cik.CoreLibs.ServiceDiscovery
{
    public interface IDiscoveryService
    {
        Task<ServiceInformation[]> GetServiceInstancesAsync(string name);
        Task KeyValuePutAsync(string key, object value);
        Task<T> KeyValueGetAsync<T>(string key);
    }
}