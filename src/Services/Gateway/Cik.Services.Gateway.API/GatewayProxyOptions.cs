using Cik.CoreLibs.Api;
using Microsoft.Extensions.Logging;

namespace Cik.Services.Gateway.API
{
    public class GatewayProxyOptions
    {
        public string ServiceName { get; set; }
        public RestClient RestClient { get; set; }
        public ILogger Logger { get; set; }
    }
}