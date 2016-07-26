using System.Net.Http;

namespace Cik.Services.Gateway.API
{
    public class ProxyOptions
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string[] RemovedPatterns { get; set; }
        public HttpMessageHandler BackChannelMessageHandler { get; set; }
    }
}