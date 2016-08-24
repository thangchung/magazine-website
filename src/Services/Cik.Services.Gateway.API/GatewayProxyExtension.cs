using System;
using Cik.CoreLibs.Api;
using Cik.CoreLibs.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cik.Services.Gateway.API
{
    public static class GatewayProxyExtension
    {
        public static IApplicationBuilder RunGatewayProxy(this IApplicationBuilder app, GatewayProxyOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<GatewayProxyMiddleware>(Options.Create(options));
        }

        public static IApplicationBuilder MapGatewayProxy(this IApplicationBuilder app, string pathMatch,
            string serviceName)
        {
            return app.Map(pathMatch,
                builder => builder.RunGatewayProxy(
                    new GatewayProxyOptions
                    {
                        ServiceName = serviceName,
                        RestClient = builder.ApplicationServices.GetService<RestClient>(),
                        Logger = LoggerHelper.GetLogger<Startup>()
                    }));
        }
    }
}