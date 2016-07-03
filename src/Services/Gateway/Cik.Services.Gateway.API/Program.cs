using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Cik.Services.Gateway.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
        .UseUrls("https://*:8080") // for docker
        .UseKestrel(options =>
        {
          // reference at https://github.com/aspnet/KestrelHttpServer/blob/dev/samples/SampleApp/Startup.cs
          options.NoDelay = true;
          options.UseHttps("magazine_server.pfx", "magazine");
          options.UseConnectionLogging();
        })
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}