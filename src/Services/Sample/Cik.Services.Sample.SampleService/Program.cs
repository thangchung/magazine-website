using System.IO;
using Cik.Api.Extensions;
using Cik.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Services.Sample.SampleService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var config = new ConfigurationBuilder()
        .BuildHostConfiguration(LoggerHelper.GetLogger<Program>());

      var host = new WebHostBuilder()
        .UseUrls(config.GetUrlForDocker("sample_service_url"))
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}