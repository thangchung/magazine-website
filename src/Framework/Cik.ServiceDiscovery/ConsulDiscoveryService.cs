using Microphone.Core;
using Microphone.Core.ClusterProviders;

namespace Cik.ServiceDiscovery
{
  public class ConsulDiscoveryService : DiscoveryService
  {
    public override void BootstrapClient()
    {
      Cluster.BootstrapClient(new ConsulProvider());
    }
  }
}