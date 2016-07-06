using System.IO;
using Cik.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Services.Gateway.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var config = new ConfigurationBuilder().BuildHostConfiguration();
      var host = new WebHostBuilder()
        .UseUrls(config.GetUrlForDocker("gateway_service_url"))
        .BuildWebHostBuilder(config)
        .UseConfiguration(config)
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}