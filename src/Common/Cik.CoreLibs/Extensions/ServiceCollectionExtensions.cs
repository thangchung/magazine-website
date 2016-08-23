using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cik.CoreLibs.Api;
using Cik.CoreLibs.Domain;
using Cik.CoreLibs.Filters;
using Cik.CoreLibs.ServiceDiscovery;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.CoreLibs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServiceCollection(this IServiceCollection services,
            IConfigurationRoot configuration, Action<IServiceCollection> additionalDependencies)
        {
            // allow caller registering
            additionalDependencies(services);

            // Add framework services.
            services.AddMvc(config => { config.Filters.Add(typeof (ValidationExceptionFilterAttribute)); });

            // Add service discovery and return 
            return services.RegisterServiceDiscovery();
        }

        public static ContainerBuilder AddCoreAutofacDependencies(this IServiceCollection services,
            Action<ContainerBuilder> additionalDependencies)
        {
            // Autofac container
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof (IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
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
            builder.RegisterType<ConsulDiscoveryService>().As<IDiscoveryService>().InstancePerLifetimeScope();
            builder.RegisterType<RestClient>().AsSelf();

            // allow caller registering
            additionalDependencies(builder);

            // populate with the service collection
            builder.Populate(services);
            return builder;
        }
    }
}