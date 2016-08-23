using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Cik.CoreLibs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cik.Services.Magazine.MagazineService.Infrastruture.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureWebHost(
            this IApplicationBuilder builder,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IConfigurationRoot configuration)
        {
            Guard.NotNull(builder);
            Guard.NotNull(env);
            Guard.NotNull(loggerFactory);
            Guard.NotNull(configuration);

            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();
            var jwtBearerOptions = new JwtBearerOptions
            {
                Authority = "https://localhost:44307",
                Audience = "https://localhost:44307/resources",
                AutomaticAuthenticate = true,

                // required if you want to return a 403 and not a 401 for forbidden responses
                AutomaticChallenge = true
            };

            // builder.UseJwtBearerAuthentication(jwtBearerOptions);
            builder.UseApplicationInsightsRequestTelemetry();
            builder.UseApplicationInsightsExceptionTelemetry();

            if (env.IsDevelopment())
            {
                builder.UseBrowserLink();

                // TODO: comment out this because the PostgreSQL issue 
                // SeedData.InitializeMagazineDatabaseAsync(app.ApplicationServices).Wait();
            }

            // use MVC and return
            return builder.UseMvc();
        }
    }
}