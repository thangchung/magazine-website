using Cik.Shared.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cik.Shared.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationRoot BuildHostConfiguration(this ConfigurationBuilder builder, ILogger logger = null)
        {
            var configFullPath = ConfigHelper.GetConfigRootPath("hosting.json", logger);
            return builder
                .AddJsonFile(configFullPath, true)
                .Build();
        }
    }
}