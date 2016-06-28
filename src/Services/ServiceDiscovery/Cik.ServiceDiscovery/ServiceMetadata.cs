namespace Cik.ServiceDiscovery
{
  public class ServiceMetadata
  {
    public string Node { get; set; }
    public string ServiceName { get; set; }
    public string Address { get; set; }
    public string ServiceAddress { get; set; }
    public int ServicePort { get; set; }
  }
}