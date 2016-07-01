using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Extensions;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.QueryModel;
using Cik.Services.Magazine.MagazineService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Cik.Services.Magazine.MagazineService
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
        .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      //Add Cors support to the service
      services.AddCors();

      var policy = new CorsPolicy();
      policy.Headers.Add("*");
      policy.Methods.Add("*");
      policy.Origins.Add("*");
      policy.SupportsCredentials = true;
      services.AddCors(x => x.AddPolicy("corsGlobalPolicy", policy));

      var guestPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("scope", "data_category_records")
                .Build();

      services.AddAuthorization(options =>
      {
        options.AddPolicy("data_category_records_admin", policyAdmin =>
        {
          policyAdmin.RequireClaim("role", "data_category_records_admin");
        });
        options.AddPolicy("data_category_records_user", policyUser =>
        {
          policyUser.RequireClaim("role", "data_category_records_user");
        });
      });

      services.AddMvc(options =>
      {
        options.Filters.Add(new AuthorizeFilter(guestPolicy));
      });

      // TODO: temporary use In-Memory for now due to the issue at https://github.com/npgsql/npgsql/issues/1171
      // Use a PostgreSQL database
      /*var sqlConnectionString = Configuration["DataAccessPostgreSqlProvider:ConnectionString"];
      services.AddDbContext<MagazineDbContext>(options =>
          options.UseNpgsql(
              sqlConnectionString,
              b => b.MigrationsAssembly("Cik.Services.Magazine.MagazineService")
              )); */

      // Create options telling the context to use an
      // InMemory database and the service provider.
      services.AddDbContext<MagazineDbContext>(options =>
        options.UseInMemoryDatabase()
        );

      // Add framework services.
      services.AddMvc();

      // Autofac container
      var builder = new ContainerBuilder();
      builder.RegisterType<MagazineDbContext>().AsSelf().SingleInstance();
      builder.RegisterType<CategoryRepository>()
        .As<IRepository<Category, Guid>>()
        .InstancePerLifetimeScope();
      builder.RegisterInstance(new InMemoryBus()).SingleInstance();
      builder.Register(x => x.Resolve<InMemoryBus>()).As<ICommandHandler>();
      builder.Register(x => x.Resolve<InMemoryBus>()).As<IDomainEventPublisher>();
      builder.Register(x => x.Resolve<InMemoryBus>()).As<IHandlerRegistrar>();
      builder.RegisterType<CategoryQueryModelFinder>().AsImplementedInterfaces().InstancePerLifetimeScope();
      builder.RegisterCommandHandlers();
      builder.Populate(services);

      // build up the container
      var container = builder.Build();
      container.RegisterHandlers(typeof (Startup));
      return container.Resolve<IServiceProvider>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseCors("corsGlobalPolicy");
      app.UseCookieAuthentication();

      if (env.IsDevelopment())
      {
        app.UseBrowserLink();

        // TODO: comment out this because the PostgreSQL issue 
        // SeedData.InitializeMagazineDatabaseAsync(app.ApplicationServices).Wait();
      }

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
      app.UseMvc();
    }
  }
}