using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cik.CoreLibs.Helpers;
using Cik.CoreLibs.ServiceDiscovery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Api
{
    public class RestClient
    {
        private readonly HttpClient _client;
        private readonly IDiscoveryService _discoveryService;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;

        public RestClient(IHostingEnvironment env, IDiscoveryService discoveryService, HttpClient client = null)
        {
            _env = env;
            _client = client ?? new HttpClient();
            _discoveryService = discoveryService;
            _logger = LoggerHelper.GetLogger<RestClient>();
        }

        public async Task<TReturnMessage> GetAsync<TReturnMessage>(string serviceName, string path)
            where TReturnMessage : class, new()
        {
            using (_client)
            {
                var infos = await _discoveryService.GetServiceInstancesAsync(serviceName);
                var defaultInfo = infos.FirstOrDefault();
                Guard.NotNull(defaultInfo);
                var host = _env.IsDevelopment() ? "192.168.99.100" : defaultInfo.Host;
                var uri = new Uri($"http://{host}:{defaultInfo.Port}{path}");

                _logger.LogInformation("[CIK INFO] Uri:" + uri);

                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc0MDIyNUQ5NERBMDYwMkFGQzlBN0NGNzNDQ0RGQjlGRDYxRkNFMkYiLCJ0eXAiOiJKV1QifQ.eyJuYmYiOjE0Njc4MDI3MzgsImV4cCI6MTQ2NzgwNjMzOCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMDciLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMwNy9yZXNvdXJjZXMiLCJjbGllbnRfaWQiOiJtYWdhemluZV93ZWJfdGVzdCIsInNjb3BlIjpbImRhdGFfY2F0ZWdvcnlfcmVjb3JkcyIsIm9wZW5pZCJdLCJzdWIiOiJ1c2VyIiwiYXV0aF90aW1lIjoxNDY3ODAyNzM3LCJpZHAiOiJpZHNydiIsInJvbGUiOlsidXNlciIsImRhdGFfY2F0ZWdvcnlfcmVjb3Jkc191c2VyIiwiZGF0YV9jYXRlZ29yeV9yZWNvcmRzIl0sImFtciI6WyJwYXNzd29yZCJdfQ.d9eRUXbkWMlALeyj1iVq47MomUaACH64YfzAR0i9Yi64AXeNT0FFQ87Id835IZLU_9ffUFjTVNgkKjtEjzMes94L_LEJ_P1e-wp8Rk2FWGkxLkJpsapho2bvZ6v1DqNRMmcMNl4F5RXaohcbCtwItulmMzmfpWX89LDgw_yLyot51lgL55K-1RHcdxTwq4V-JPIHcM8LKhZj9Ky_5-bQb1Hpd2zAghTXT7tKNNSeEWe2hnag3M8p9pNqJaoNUYGnP588rNEfz7KsiI6ldLprmW8LiVRWOu1zXroZQ2C2RF3HFJ-kSrPDCdXiZ0ae35Q1CnxCDkcLHwpdbAs01dMApg");

                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TReturnMessage>();
                }
            }
            return await Task.FromResult(new TReturnMessage());
        }

        public async Task<HttpResponseMessage> GetAsync(string serviceName, string path)
        {
            var infos = await _discoveryService.GetServiceInstancesAsync(serviceName);
            var defaultInfo = infos.FirstOrDefault();
            Guard.NotNull(defaultInfo);
            var host = _env.IsDevelopment() ? "192.168.99.100" : defaultInfo.Host;
            var uri = new Uri($"http://{host}:{defaultInfo.Port}{path}");

            _logger.LogInformation("[CIK INFO] Uri:" + uri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await _client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpContext proxyContext, string serviceName, string path)
        {
            var requestMessage = new HttpRequestMessage();
            var infos = await _discoveryService.GetServiceInstancesAsync(serviceName);
            var defaultInfo = infos.FirstOrDefault();
            Guard.NotNull(defaultInfo);

            // TODO: docker-machine host should get from the config
            var host = _env.IsDevelopment() ? "192.168.99.100" : defaultInfo.Host;
            var uri = new Uri($"http://{host}:{defaultInfo.Port}{path}");

            _logger.LogInformation("[CIK INFO] Uri:" + uri);

            if (!string.Equals(proxyContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(proxyContext.Request.Method, "HEAD", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(proxyContext.Request.Method, "DELETE", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(proxyContext.Request.Method, "TRACE", StringComparison.OrdinalIgnoreCase))
            {
                var streamContent = new StreamContent(proxyContext.Request.Body);
                requestMessage.Content = streamContent;
            }

            // Copy the request headers
            foreach (var header in proxyContext.Request.Headers)
            {
                if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                {
                    requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            requestMessage.Headers.Host = host;
            requestMessage.RequestUri = uri;
            requestMessage.Method = new HttpMethod(proxyContext.Request.Method);

            return await _client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, proxyContext.RequestAborted);
        }
    }
}