using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
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
        builder.AddApplicationInsightsSettings(true);
      }

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddApplicationInsightsTelemetry(Configuration);
      services.AddAuthentication();

      //Add Cors support to the service
      services.AddCors();

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

      app.UseApplicationInsightsRequestTelemetry();
      app.UseApplicationInsightsExceptionTelemetry();

      app.UseCors("corsGlobalPolicy");
      app.UseCookieAuthentication();

      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();
      var jwtBearerOptions = new JwtBearerOptions()
      {
        Authority = "https://localhost:44307",
        Audience = "https://localhost:44307/resources",
        AutomaticAuthenticate = true,

        // required if you want to return a 403 and not a 401 for forbidden responses
        AutomaticChallenge = true
      };

      app.UseJwtBearerAuthentication(jwtBearerOptions);

      // Reverse Proxy
      // Get the config and forward the request into the real host behind it.
      var uriConfig = Configuration.GetSection("HostUri");
      app.RunProxy(new ProxyOptions
      {
        Scheme = uriConfig["Schema"],
        Host = uriConfig["Host"],
        Port = uriConfig["Port"],
        // BackChannelMessageHandler = new SubDomainMessageHandler()
      });
    }
  }

  // TODO: 
  // parse the URI if contains the subdomain then forward it into the correct one service
  // maybe we can use the regular expression for make it easier
  // https://github.com/aspnet/Proxy/pull/12
  // https://github.com/chsword/Proxy/tree/dev-multiproxy
  internal class SubDomainMessageHandler : HttpMessageHandler
  {
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      return Task.FromResult<HttpResponseMessage>(null);
    }
  }
}