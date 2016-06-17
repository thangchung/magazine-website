using Microsoft.Extensions.DependencyInjection;

namespace Cik.ServiceDiscovery
{
    public class ConsulServiceBuilder
    {
        public IServiceCollection ServiceCollection { get; }

        public ConsulServiceBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }
    }
}