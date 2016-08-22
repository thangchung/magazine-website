using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cik.CoreLibs.Api;
using Cik.CoreLibs.Domain;
using Cik.CoreLibs.Extensions;
using Cik.CoreLibs.Filters;
using Cik.CoreLibs.ServiceDiscovery;
using Cik.Services.Magazine.MagazineService.Features.Category;
using Cik.Services.Magazine.MagazineService.Features.Category.Commands;
using Cik.Services.Magazine.MagazineService.Features.Category.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.Services.Magazine.MagazineService.Infrastruture
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer AddWebHost(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var registeredServices = services.AddHost(internalServices =>
            {
                internalServices.AddAuthorization();
                internalServices.AddServiceCollection(configuration);
            });
            return registeredServices
                .AddAutofacDependencies()
                .Build();
        }

        public static void AddAuthorization(this IServiceCollection services)
        {
            var guestPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("scope", "data_category_records")
                .Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("data_category_records_admin",
                    policyAdmin => { policyAdmin.RequireClaim("role", "data_category_records_admin"); });
                options.AddPolicy("data_category_records_user",
                    policyUser => { policyUser.RequireClaim("role", "data_category_records_user"); });
            });

            // services.AddMvc(options => { options.Filters.Add(new AuthorizeFilter(guestPolicy)); });
        }

        public static IServiceCollection AddServiceCollection(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            if (!configuration["RunInMemory"].ToBoolean())
            {
                // Use a PostgreSQL database
                var sqlConnectionString = configuration["DataAccessPostgreSqlProvider:ConnectionString"];
                services.AddDbContext<MagazineDbContext>(options =>
                    options.UseNpgsql(
                        sqlConnectionString,
                        b => b.MigrationsAssembly("Cik.Services.Magazine.MagazineService")
                        ));
            }
            else
            {
                // Use a InMemory database
                services.AddDbContext<MagazineDbContext>(options =>
                    options.UseInMemoryDatabase()
                    );
            }

            // Add framework services.
            services.AddMvc(config => { config.Filters.Add(typeof (ValidationExceptionFilterAttribute)); });

            // Add service discovery and return 
            return services.RegisterServiceDiscovery();
        }

        public static ContainerBuilder AddAutofacDependencies(this IServiceCollection services)
        {
            // Autofac container
            var builder = new ContainerBuilder();
            builder.RegisterType<MagazineDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<CategoryRepository>()
                .As<IRepository<Category, Guid>>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof (IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof (CreateCategoryCommand).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof (ICommandValidator<>).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>) c.Resolve(typeof (IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterType<SimpleCommandBus>().As<ICommandBus>();

            builder.Populate(services);
            builder.RegisterType<ConsulDiscoveryService>()
                .As<IDiscoveryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RestClient>().AsSelf();
            return builder;
        }
    }
}