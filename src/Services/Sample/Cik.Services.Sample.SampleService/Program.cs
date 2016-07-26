using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Cik.Shared.Api.Extensions;
using Cik.Shared.Core;

namespace Cik.Services.Sample.SampleService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .BuildHostConfiguration(LoggerHelper.GetLogger<Program>());

            var host = new WebHostBuilder()
                .UseUrls(config.GetUrlForDocker("sample_service_url"))
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}