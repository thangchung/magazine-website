using System.Net.Http;
using Cik.Shared.Rest;
using Microsoft.Extensions.Logging;

namespace Cik.Services.Gateway.API
{
    public class ProxyOptions
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string[] RemovedPatterns { get; set; }
        public HttpMessageHandler BackChannelMessageHandler { get; set; }
        public RestClient RestClient { get; set; }
        public ILogger Logger { get; set; }
    }
}