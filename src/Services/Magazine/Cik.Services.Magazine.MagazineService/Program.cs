using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Cik.Services.Magazine.MagazineService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
        .UseUrls("http://*:5000") // for docker
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}