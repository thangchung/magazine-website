using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.CoreLibs.Extensions
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
                    config.GetCertificationFilePath(),
                    config.GetCertificationPassword()
                    );
                options.UseConnectionLogging();
            });
        }
    }
}