using Cik.Core;
using Microsoft.Extensions.Configuration;

namespace Cik.Api.Extensions
{
  public static class ConfigurationExtensions
  {
    public static string GetCertificationFilePath(this IConfigurationRoot config)
    {
      return ConfigHelper.GetConfigRootPath() + "\\" + config.GetValue<string>("certification:file");
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