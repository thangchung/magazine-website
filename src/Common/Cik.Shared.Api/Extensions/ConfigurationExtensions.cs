using Cik.Shared.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cik.Shared.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetCertificationFilePath(this IConfigurationRoot config, ILogger logger = null)
        {
            return ConfigHelper.GetConfigRootPath(config.GetValue<string>("certification:file"), logger);
        }

        public static string GetCertificationPassword(this IConfigurationRoot config)
        {
            return config.GetValue<string>("certification:password");
        }

        public static string GetUrlForDocker(this IConfigurationRoot config, string urls)
        {
            // for docker
            return config.GetValue<string>($"hosts:{urls}:urls");
        }
    }
}