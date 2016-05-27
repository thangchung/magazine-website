using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.Infrastruture
{
    public interface IExtension
    {
        string Name { get; }

        void SetConfigurationRoot(IConfigurationRoot configurationRoot);
        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder applicationBuilder);
        void RegisterRoutes(IRouteBuilder routeBuilder);
    }
}