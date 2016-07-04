using System.IO;
using Cik.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Services.Magazine.MagazineService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var config = new ConfigurationBuilder().BuildHostConfiguration();
      var host = new WebHostBuilder()
        .UseUrls(config.GetValue<string>("hosts:magazineService:urls")) // for docker
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}