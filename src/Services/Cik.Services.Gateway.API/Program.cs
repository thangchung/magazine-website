using System.IO;
using Cik.CoreLibs.Extensions;
using Cik.CoreLibs.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cik.Services.Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .BuildHostConfiguration(LoggerHelper.GetLogger<Program>());

            var host = new WebHostBuilder()
                .UseUrls(config.GetUrlForDocker("gateway_service_url"))
                .BuildWebHostBuilder(config)
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}