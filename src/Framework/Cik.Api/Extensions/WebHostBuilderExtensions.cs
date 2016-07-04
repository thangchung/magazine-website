using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Api.Extensions
{
  public static class WebHostBuilderExtensions
  {
    public static IWebHostBuilder BuildWebHostBuilder(this IWebHostBuilder builder, IConfigurationRoot config)
    {
      return builder.UseKestrel(options =>
      {
        // reference at https://github.com/aspnet/KestrelHttpServer/blob/dev/samples/SampleApp/Startup.cs
        options.NoDelay = true;
        options.UseHttps(
          config.GetValue<string>("certification:file"),
          config.GetValue<string>("certification:password")
          );
        options.UseConnectionLogging();
      });
    }
  }
}