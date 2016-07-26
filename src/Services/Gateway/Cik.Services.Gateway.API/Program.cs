using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Cik.Shared.Api.Extensions;

namespace Cik.Services.Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().BuildHostConfiguration();
            var host = new WebHostBuilder()
                .UseUrls(config.GetUrlForDocker("gateway_service_url"))
                // .BuildWebHostBuilder(config)
                // .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}