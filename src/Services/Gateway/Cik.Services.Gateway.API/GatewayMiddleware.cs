using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cik.Shared.Rest;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cik.Services.Gateway.API
{
    public class GatewayMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProxyOptions _options;
        private readonly RestClient _restClient;
        private readonly ILogger _logger;

        public GatewayMiddleware(RequestDelegate next, IOptions<ProxyOptions> options)
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
            _restClient = _options.RestClient;
            _logger = _options.Logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            HttpResponseMessage responseMessage = null;
            var serviceNames = new List<string> {"sample_service", "/magazine_service"};
            foreach (var serviceName in serviceNames)
            {
                if (!request.Path.HasValue) continue;
                if (!request.Path.Value.Contains(serviceName)) continue;
                _logger.LogInformation("[CIK INFO] Run: " + serviceName + " service");
                var index = Regex.Match(request.Path.Value, serviceName, RegexOptions.RightToLeft).Index + serviceName.Length;
                _logger.LogInformation("[CIK INFO] Index: " + index);
                var subPath =request.Path.Value.Substring(index, request.Path.Value.Length - index);
                _logger.LogInformation("[CIK INFO] Subpath: " + subPath);
                responseMessage = await _restClient.Get(serviceName, subPath);
                break;
            }

            if (responseMessage != null)
            {
                context.Response.Headers.Remove("transfer-encoding");
                await responseMessage.Content.CopyToAsync(context.Response.Body);
            }
        }
    }
}