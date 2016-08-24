using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cik.Services.Gateway.API
{
    public class GatewayProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GatewayProxyOptions _options;

        public GatewayProxyMiddleware(RequestDelegate next, IOptions<GatewayProxyOptions> options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            // _logger.LogInformation("[CIK INFO] Run: " + _serviceName + " service");
            _options.Logger.LogInformation("[CIK INFO] Subpath: " + context.Request.Path.Value);

            var responseMessage = await _options.RestClient.SendAsync(context, _options.ServiceName, context.Request.Path.Value);
            using (responseMessage)
            {
                if (responseMessage != null)
                {
                    context.Response.StatusCode = (int)responseMessage.StatusCode;
                    foreach (var header in responseMessage.Headers)
                    {
                        context.Response.Headers[header.Key] = header.Value.ToArray();
                    }

                    foreach (var header in responseMessage.Content.Headers)
                    {
                        context.Response.Headers[header.Key] = header.Value.ToArray();
                    }

                    context.Response.Headers.Remove("transfer-encoding");
                    await responseMessage.Content.CopyToAsync(context.Response.Body);
                }
            }
        }
    }
}