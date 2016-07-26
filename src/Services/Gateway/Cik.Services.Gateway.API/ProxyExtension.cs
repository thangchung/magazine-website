using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Cik.Services.Gateway.API
{
    public static class ProxyExtension
    {
        public static IApplicationBuilder RunProxy(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ProxyMiddleware>();
        }

        public static IApplicationBuilder RunProxy(this IApplicationBuilder app, ProxyOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<ProxyMiddleware>(Options.Create(options));
        }
    }
}