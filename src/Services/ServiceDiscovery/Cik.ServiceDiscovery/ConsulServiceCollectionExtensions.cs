using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.ServiceDiscovery
{
    public static class ConsulServiceCollectionExtensions
    {
        public static ConsulServiceBuilder AddConsul([NotNull] this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IServiceDiscovery, ConsulServiceDiscovery>();

            return new ConsulServiceBuilder(serviceCollection);
        }
    }
}