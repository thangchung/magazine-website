using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cik.CoreLibs.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureCoreWebHost(
            this IApplicationBuilder builder,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IConfigurationRoot configuration,
             Action<IApplicationBuilder> additionalDependencies = null)
        {
            Guard.NotNull(builder);
            Guard.NotNull(env);
            Guard.NotNull(loggerFactory);
            Guard.NotNull(configuration);

            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // allow caller registering
            additionalDependencies?.Invoke(builder);

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