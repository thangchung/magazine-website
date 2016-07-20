using Microphone;

namespace Cik.ServiceDiscovery
{
  public class ConsulDiscoveryService : DiscoveryService
  {
    public ConsulDiscoveryService(IClusterClient client) : base(client)
    {
    }

    public override void BootstrapClient()
    {
      // Cluster.Client(new ConsulProvider("192.168.99.100"));
    }
  }
}