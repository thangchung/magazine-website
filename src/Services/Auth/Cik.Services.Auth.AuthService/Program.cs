using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Services.Auth.AuthService
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
        .UseUrls(hostConfig.GetValue<string>("urls"))
        .UseKestrel(options =>
        {
          options.NoDelay = true;
          options.UseHttps(
            cerConfig.GetValue<string>("file"),
            cerConfig.GetValue<string>("password")
            );
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