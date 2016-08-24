using Autofac;
using Cik.CoreLibs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cik.CoreLibs.Extensions;

namespace Cik.Services.Sample.SampleService.Infrastruture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer AddWebHost(this IServiceCollection services, IConfigurationRoot configuration)
        {
            Guard.NotNull(services);
            Guard.NotNull(configuration);

            return services
                .AddCoreServiceCollection(configuration, s => s.AddServiceCollection(configuration))
                .AddCoreAutofacDependencies(s => s.AddAutofacDependencies())
                .Build();
        }

        private static void AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
        }

        private static void AddAutofacDependencies(this ContainerBuilder builder)
        {
        }
    }
}