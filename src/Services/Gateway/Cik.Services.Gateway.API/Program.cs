using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Services.Gateway.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("hosting.json", true)
        .Build();

      var hostConfig = config.GetSection("host");
      var cerConfig = config.GetSection("certification");

      var host = new WebHostBuilder()
        .UseUrls(hostConfig.GetValue<string>("urls")) // for docker
        .UseKestrel(options =>
        {
          // reference at https://github.com/aspnet/KestrelHttpServer/blob/dev/samples/SampleApp/Startup.cs
          options.NoDelay = true;
          options.UseHttps(
            cerConfig.GetValue<string>("file"),
            cerConfig.GetValue<string>("password")
            );
          options.UseConnectionLogging();
        })
        .UseConfiguration(config)
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}