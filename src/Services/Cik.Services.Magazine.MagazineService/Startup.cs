using System;
using Autofac;
using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Extensions;
using Cik.Services.Magazine.MagazineService.Infrastruture.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cik.Services.Magazine.MagazineService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = env.BuildDefaultConfiguration();
        }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.AddWebHost(Configuration).Resolve<IServiceProvider>();
            // call Subscribe() to listen the command handlers
            serviceProvider.GetService<ICommandConsumer>().Subscriber.Subscribe();
            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        { 
            app.ConfigureWebHost(env, loggerFactory, Configuration);
        }
    }
}