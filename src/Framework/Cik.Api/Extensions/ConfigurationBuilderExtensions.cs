using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cik.Api.Extensions
{
  public static class ConfigurationBuilderExtensions
  {
    public static IConfigurationRoot BuildHostConfiguration(this ConfigurationBuilder builder)
    {
      var fullPath = Directory.GetParent("..\\..\\..\\");
      return builder
        .AddJsonFile(fullPath + "\\Config\\hosting.json", true)
        .Build();
    }
  }
}