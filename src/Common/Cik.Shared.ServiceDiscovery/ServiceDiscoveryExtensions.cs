using Microphone.AspNet;
using Microphone.Consul;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static IServiceCollection RegisterServiceDiscovery(this IServiceCollection services)
        {
            services.AddMicrophone<ConsulProvider>();
            services.Configure<ConsulOptions>(o => { o.Host = "192.168.99.100"; //TODO: change to config
            });

            return services;
        }
    }
}