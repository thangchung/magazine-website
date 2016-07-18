using System.IO;
using Microsoft.Extensions.Logging;

namespace Cik.Core
{
  public static class ConfigHelper
  {
    public static string GetConfigRootPath(string fileName = null, ILogger logger = null)
    {
      var di = fileName == null ? new DirectoryInfo("../../../Config/") : new DirectoryInfo("../../../Config/" + fileName);
      logger?.LogInformation("[CIK INFO] " + di.FullName);
      return di.FullName;
    }
  }
}