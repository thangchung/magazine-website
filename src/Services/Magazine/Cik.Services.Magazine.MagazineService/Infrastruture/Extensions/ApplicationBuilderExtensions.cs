using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Cik.CoreLibs.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
            return builder
                .ConfigureCoreWebHost(
                    env,
                    loggerFactory,
                    configuration,
                    b =>
                    {
                        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();
                        var jwtBearerOptions = new JwtBearerOptions
                        {
                            Authority = "https://localhost:44307",
                            Audience = "https://localhost:44307/resources",
                            AutomaticAuthenticate = true,

                            // required if you want to return a 403 and not a 401 for forbidden responses
                            AutomaticChallenge = true
                        };

                        // b.UseJwtBearerAuthentication(jwtBearerOptions);
                        /*if (env.IsDevelopment())
                        {
                            // TODO: comment out this because the PostgreSQL issue 
                            // SeedData.InitializeMagazineDatabaseAsync(app.ApplicationServices).Wait();
                        } */
                    });
        }
    }
}