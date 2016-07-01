using System.IO;
using System.Security.Cryptography.X509Certificates;
using Cik.Services.Auth.AuthService.Configuration;
using Cik.Services.Auth.AuthService.UI;
using Cik.Services.Auth.AuthService.UI.Login;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cik.Services.Auth.AuthService
{
  public class Startup
  {
    private readonly IHostingEnvironment _environment;

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
        .AddEnvironmentVariables();

      if (env.IsDevelopment())
      {
        // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
        builder.AddApplicationInsightsSettings(true);
      }
      Configuration = builder.Build();
      _environment = env;
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var cert = new X509Certificate2(
        Path.Combine(_environment.ContentRootPath, "magazine_server.pfx"),
        "magazine");

      services.AddIdentityServer()
        .SetSigningCredential(cert)
        .AddInMemoryClients(Clients.Get())
        .AddInMemoryScopes(Scopes.Get())
        .AddInMemoryUsers(Users.Get());

      // Add framework services.
      services.AddApplicationInsightsTelemetry(Configuration);

      // for the UI
      services
        .AddMvc()
        .AddRazorOptions(razor => { razor.ViewLocationExpanders.Add(new CustomViewLocationExpander()); });
      services.AddTransient<LoginService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseApplicationInsightsRequestTelemetry();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseCookieAuthentication(
        new CookieAuthenticationOptions
        {
          AuthenticationScheme = "Temp",
          AutomaticAuthenticate = false,
          AutomaticChallenge = false
        });

      app.UseGoogleAuthentication(
        new GoogleOptions
        {
          AuthenticationScheme = "Google",
          SignInScheme = "Temp",
          ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com",
          ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo"
        });

      app.UseIdentityServer();
      app.UseApplicationInsightsExceptionTelemetry();
      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();
    }
  }
}