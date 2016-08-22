using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.CoreLibs.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot BuildDefaultConfiguration(this IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}