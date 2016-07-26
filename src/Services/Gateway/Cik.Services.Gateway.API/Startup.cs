using System;
using Cik.Shared.Core;
using Cik.Shared.Rest;
using Cik.Shared.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cik.Services.Gateway.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                // builder.AddApplicationInsightsSettings(true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IConfiguration HostConfiguration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // services.AddApplicationInsightsTelemetry(Configuration);

            //Add Cors support to the service
            services.AddCors();

            services.RegisterServiceDiscovery();
            services.AddScoped<IDiscoveryService, ConsulDiscoveryService>();
            services.AddTransient<RestClient, RestClient>();

            var policy = new CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            services.AddCors(x => x.AddPolicy("corsGlobalPolicy", policy));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // app.UseApplicationInsightsRequestTelemetry();
            // app.UseApplicationInsightsExceptionTelemetry();

            app.UseCors("corsGlobalPolicy");

            // app.RunGatewayProxy();

            /*app.Run(async context =>
            {
                await context.Response.WriteAsync("hello");
            });*/

            app.RunGatewayProxy(new ProxyOptions
            {
                RestClient = app.ApplicationServices.GetService<RestClient>(),
                Logger = LoggerHelper.GetLogger<Startup>()
            });

            /*app.Map("/sample_service",
                builder => builder.RunGatewayProxy(
                    new ProxyOptions
                    {
                        RestClient = app.ApplicationServices.GetService<RestClient>(),
                        Logger = LoggerHelper.GetLogger<Startup>()
                    }));*/

            // TODO: not too good, but now I only want to focus on the important things
            // Reverse Proxy
            // Get the config and forward the request into the real host behind it.
            /*HostConfiguration = Configuration.GetSection("HostUris");
            app.MapWhen(IsAuthPath,
                appBuilder => { appBuilder.RunProxy(BuildProxyOptions(HostConfiguration.GetSection("auth"))); });
            app.MapWhen(IsMagazinePath,
                appBuilder => { appBuilder.RunProxy(BuildProxyOptions(HostConfiguration.GetSection("magazine"))); });*/
        }

        /*private static bool IsAuthPath(HttpContext httpContext)
        {
            return httpContext.Request.Path.Value.StartsWith(@"/auth/", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsMagazinePath(HttpContext httpContext)
        {
            return httpContext.Request.Path.Value.StartsWith(@"/magazine/", StringComparison.OrdinalIgnoreCase);
        }

        private static ProxyOptions BuildProxyOptions(IConfiguration config)
        {
            var proxyOptions = new ProxyOptions
            {
                Scheme = config.GetValue<string>("schema"),
                Host = config.GetValue<string>("host"),
                Port = config.GetValue<string>("port"),
                RemovedPatterns = new[] {@"/auth/", @"/magazine/"}
            };

            return proxyOptions;
        } */
    }
}