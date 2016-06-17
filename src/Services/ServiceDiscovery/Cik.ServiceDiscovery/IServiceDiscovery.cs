using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cik.ServiceDiscovery
{
    public interface IServiceDiscovery
    {
        Task RegisterService(string serviceName, string endpoint, int port);
        Task RemoveService(string serviceName, string endpoint, int port);
        Task<List<ServiceMetadata>> GetService(string serviceName);
    }
}