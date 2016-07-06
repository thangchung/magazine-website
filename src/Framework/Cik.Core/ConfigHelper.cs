using System.IO;

namespace Cik.Core
{
  public static class ConfigHelper
  {
    public static string GetConfigRootPath()
    {
      return Directory.GetParent("..\\..\\..\\Config\\").ToString();
    }
  }
}