using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Cik.CoreLibs.Extensions;

namespace Cik.Services.Magazine.MagazineService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().BuildHostConfiguration();
            var host = new WebHostBuilder()
                .UseUrls(config.GetUrlForDocker("magazine_service_url"))
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}